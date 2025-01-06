using financesFlow.Aplicacao.Reports;
using financesFlow.Aplicacao.useCase.Arquivo.Pdf.Colors;
using financesFlow.Aplicacao.useCase.Arquivo.Pdf.Fonts;
using financesFlow.Dominio.Repositories.Despesas;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace financesFlow.Aplicacao.useCase.Arquivo.Pdf;
public class GeraArquivoPdfDespesaUseCase : IGeraArquivoPdfDespesaUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IRepositorioDespesaSomenteLeitura _repositorio;

    public GeraArquivoPdfDespesaUseCase(IRepositorioDespesaSomenteLeitura repositorio)
    {
        _repositorio = repositorio;
        GlobalFontSettings.FontResolver = new ResolverFontsDespesas();

    }
    public async Task<byte[]> GeraArquivo(DateOnly mes)
    {
        var despesas = await _repositorio.FiltraPorMes(mes);
        if (despesas.Count == 0)
        {
            return [];
        }

        var documento = CriaDocumentoPDF(mes);
        var pagina = CriaPaginaDoPdf(documento);

        CriaCabecalhoComFoto(pagina);

        var totalDespesas = despesas.Sum(desp => desp.ValorDespesa);
        CriaParagrafo(pagina, mes, totalDespesas);

        foreach(var despesa in despesas)
        {
            var table = CriaTabelaDespesas(pagina);
            var linha = table.AddRow();
            linha.Height = 25;

            linha.Cells[0].AddParagraph(despesa.NomeDespesa);
            linha.Cells[0].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelpers.BLACK};
            linha.Cells[0].Shading.Color = ColorHelpers.RED_LIGHT;
            linha.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            linha.Cells[0].MergeRight = 2;
            linha.Cells[0].Format.LeftIndent = 20;

            linha.Cells[3].AddParagraph(ResourceReportGenerationMessages.VALOR);
            linha.Cells[3].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelpers.WHITE };
            linha.Cells[3].Shading.Color = ColorHelpers.RED_DARK;
            linha.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            linha = table.AddRow();
            linha.Height = 25;

            linha.Cells[0].AddParagraph(despesa.DataDespesa.ToString("D"));
            linha.Cells[0].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorHelpers.BLACK };
            linha.Cells[0].Shading.Color = ColorHelpers.GREEN_DARK;
            linha.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            linha.Cells[0].Format.LeftIndent = 20;

            linha.Cells[1].AddParagraph(despesa.DataDespesa.ToString("t"));
            linha.Cells[1].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorHelpers.BLACK };
            linha.Cells[1].Shading.Color = ColorHelpers.GREEN_DARK;
            linha.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            linha.Cells[3].AddParagraph($"{CURRENCY_SYMBOL} {despesa.ValorDespesa}");
            linha.Cells[3].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorHelpers.BLACK };
            linha.Cells[3].Shading.Color = ColorHelpers.WHITE;
            linha.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            linha = table.AddRow();
            linha.Height = 30;
            linha.Borders.Visible = false;
        }

        return RenderizaPDF(documento);
    }

    private Document CriaDocumentoPDF(DateOnly mes)
    {
        var documento = new Document();
        documento.Info.Title = $"{ResourceReportGenerationMessages.TITULO} {mes.ToString("Y")}";
        documento.Info.Author = "Eduardo Dahmer Correa";

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

    private void CriaCabecalhoComFoto(Section pagina)
    {
        var tabela = pagina.AddTable();
        tabela.AddColumn();
        tabela.AddColumn("300");

        var linha = tabela.AddRow();
        var assembly = Assembly.GetExecutingAssembly();
        var diretorio = Path.GetDirectoryName(assembly.Location);
        var caminhoImagem = Path.Combine(diretorio!, "images", "perfil.png");

        linha.Cells[0].AddImage(caminhoImagem);
        linha.Cells[1].AddParagraph("Olá, Eduardo Dahmer");
        linha.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        linha.Cells[1].VerticalAlignment = VerticalAlignment.Center;
    }

    private void CriaParagrafo(Section pagina, DateOnly mes, decimal totalDespesas)
    {
        var paragrafo = pagina.AddParagraph();
        paragrafo.Format.SpaceBefore = "40";
        paragrafo.Format.SpaceAfter = "40";
        var titulo = string.Format(ResourceReportGenerationMessages.TOTAL_GASTO_EM, mes.ToString("Y"));

        paragrafo.AddFormattedText(titulo, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragrafo.AddLineBreak();

        
        paragrafo.AddFormattedText($"{CURRENCY_SYMBOL} {totalDespesas}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
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
}
