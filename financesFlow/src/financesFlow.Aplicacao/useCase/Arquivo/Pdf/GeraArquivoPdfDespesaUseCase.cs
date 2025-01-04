using financesFlow.Aplicacao.Reports;
using financesFlow.Aplicacao.useCase.Arquivo.Pdf.Fonts;
using financesFlow.Dominio.Repositories.Despesas;
using MigraDoc.DocumentObjectModel;
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
        linha.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
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
}
