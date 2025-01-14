using financesFlow.Aplicacao.useCase.Arquivo.Pdf.Colors;
using financesFlow.Aplicacao.useCase.Arquivo.Pdf.Fonts;
using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Extensões;
using financesFlow.Dominio.Reports;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Dominio.Services.LoggedUser;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;
using Cell = MigraDoc.DocumentObjectModel.Tables.Cell;
using Table = MigraDoc.DocumentObjectModel.Tables.Table;

namespace financesFlow.Aplicacao.useCase.Arquivo.Pdf;
public class GeraArquivoPdfDespesaUseCase : IGeraArquivoPdfDespesaUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private const int HEIGHT_TABELA_DESPESAS = 25;
    private readonly IRepositorioDespesaSomenteLeitura _repositorio;
    private readonly ILoggedUser _loggedUser;

    public GeraArquivoPdfDespesaUseCase(IRepositorioDespesaSomenteLeitura repositorio, ILoggedUser loggedUser)
    {
        _repositorio = repositorio;
        _loggedUser = loggedUser;
        GlobalFontSettings.FontResolver = new ResolverFontsDespesas();

    }
    public async Task<byte[]> GeraArquivo(DateOnly mes)
    {
        var loggedUser = await _loggedUser.Get();

        var despesas = await _repositorio.FiltraPorMes(loggedUser, mes);

        if (despesas.Count == 0)
        {
            return [];
        }

        var documento = CriaDocumentoPDF(loggedUser.Nome, mes);
        var pagina = CriaPaginaDoPdf(documento);

        CriaCabecalhoComFoto(loggedUser.Nome, pagina);

        var totalDespesas = despesas.Sum(desp => desp.ValorDespesa);
        CriaParagrafo(pagina, mes, totalDespesas);

        foreach(var despesa in despesas)
        {
            var table = CriaTabelaDespesas(pagina);
            var linha = table.AddRow();
            linha.Height = HEIGHT_TABELA_DESPESAS;

            linha.Cells[0].AddParagraph(despesa.NomeDespesa);
            linha.Cells[0].Format.Font = new MigraDoc.DocumentObjectModel.Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelpers.BLACK};
            linha.Cells[0].Shading.Color = ColorHelpers.RED_LIGHT;
            linha.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            linha.Cells[0].MergeRight = 2;
            linha.Cells[0].Format.LeftIndent = 20;
            linha.Height = HEIGHT_TABELA_DESPESAS;

            linha.Cells[3].AddParagraph(ResourceReportGenerationMessages.VALOR);
            SetaEstiloBaseParaInformacaoDespesa(linha.Cells[3]);

            linha = table.AddRow();
            linha.Height = HEIGHT_TABELA_DESPESAS;

            linha.Cells[0].AddParagraph(despesa.DataDespesa.ToString("D"));
            SetaEstiloBaseParaInformacaoDespesa(linha.Cells[0]);
            linha.Cells[0].Format.LeftIndent = 20;

            linha.Cells[1].AddParagraph(despesa.DataDespesa.ToString("t"));
            SetaEstiloBaseParaInformacaoDespesa(linha.Cells[1]);

            linha.Cells[2].AddParagraph(despesa.MetodoPagamento.TipoMetodoPagamentoToString());
            SetaEstiloBaseParaInformacaoDespesa(linha.Cells[2]);

            AdicionaValorParaDespesa(linha.Cells[3], despesa.ValorDespesa);

            if (string.IsNullOrWhiteSpace(despesa.Descricao) == false)
            {
                var linhaDescricao = table.AddRow();
                linhaDescricao.Height = HEIGHT_TABELA_DESPESAS;
                linhaDescricao.Cells[0].AddParagraph(despesa.Descricao);
                linhaDescricao.Cells[0].Format.Font = new MigraDoc.DocumentObjectModel.Font { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorHelpers.BLACK };
                linhaDescricao.Cells[0].Shading.Color = ColorHelpers.GREEN_LIGHT;
                linhaDescricao.Cells[0].MergeRight = 2;
                linhaDescricao.Cells[0].Format.LeftIndent = 20;

                linha.Cells[3].MergeDown = 1;
                
            }
            AdicionaEspacoEmBranco(table);
        }

        return RenderizaPDF(documento);
    }

    private Document CriaDocumentoPDF(string author,DateOnly mes)
    {
        var documento = new Document();
        documento.Info.Title = $"{ResourceReportGenerationMessages.TITULO} {mes.ToString("Y")}";
        documento.Info.Author = author;

        var style = documento.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return documento;
    }

    private Section CriaPaginaDoPdf(Document documento)
    {
        var section = documento.AddSection();
        section.PageSetup = documento.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private byte[] RenderizaPDF(Document documento)
    {
        var renderPDF = new PdfDocumentRenderer
        {
            Document = documento,
        };

        renderPDF.RenderDocument();
        using var file = new MemoryStream();
        renderPDF.PdfDocument.Save(file);

        return file.ToArray();
    }

    private void CriaCabecalhoComFoto(string name, Section pagina)
    {
        var tabela = pagina.AddTable();
        tabela.AddColumn();
        tabela.AddColumn("300");

        var linha = tabela.AddRow();
        var assembly = Assembly.GetExecutingAssembly();
        var diretorio = Path.GetDirectoryName(assembly.Location);

        var caminhoImagem = Path.Combine(diretorio!, "images", "perfil.png");

        if(File.Exists(caminhoImagem))
        {
            var imagem = linha.Cells[0].AddImage(caminhoImagem);
            imagem.LockAspectRatio = true;
            imagem.Width = "500pt"; // Ajuste a largura conforme necessário
            imagem.Height = "500pt";
        }
        else
        {
            linha.Cells[0].AddParagraph("Imagem não encontradaS");
        }

        linha.Cells[1].AddParagraph($"Olá, {name}");
        linha.Cells[1].Format.Font = new MigraDoc.DocumentObjectModel.Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        linha.Cells[1].VerticalAlignment = VerticalAlignment.Center;
    }

    private void CriaParagrafo(Section pagina, DateOnly mes, decimal totalDespesas)
    {
        var paragrafo = pagina.AddParagraph();
        paragrafo.Format.SpaceBefore = "40";
        paragrafo.Format.SpaceAfter = "40";
        var titulo = string.Format(ResourceReportGenerationMessages.TOTAL_GASTO_EM, mes.ToString("Y"));

        paragrafo.AddFormattedText(titulo, new MigraDoc.DocumentObjectModel.Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragrafo.AddLineBreak();

        
        paragrafo.AddFormattedText($"{CURRENCY_SYMBOL} {totalDespesas:f2}", new MigraDoc.DocumentObjectModel.Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    private Table CriaTabelaDespesas(Section pagina)
    {
        var table = pagina.AddTable();
        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }

    private void SetaEstiloBaseParaInformacaoDespesa(Cell celula)
    {
        celula.Format.Font = new MigraDoc.DocumentObjectModel.Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorHelpers.BLACK };
        celula.Shading.Color = ColorHelpers.GREEN_DARK;
        celula.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AdicionaValorParaDespesa(Cell cell, decimal valor)
    {
        cell.AddParagraph($"{CURRENCY_SYMBOL} {valor:f2}");
        cell.Format.Font = new MigraDoc.DocumentObjectModel.Font { Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorHelpers.BLACK };
        cell.Shading.Color = ColorHelpers.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AdicionaEspacoEmBranco(Table tabela)
    {
        var linha = tabela.AddRow();
        linha.Height = 30;
        linha.Borders.Visible = false;
    }
}
