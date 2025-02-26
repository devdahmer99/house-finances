﻿
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using financesFlow.Aplicacao.Reports;
using financesFlow.Dominio.Enums;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Dominio.Services.LoggedUser;

namespace financesFlow.Aplicacao.useCase.Arquivo.Excel;
public class GeraArquivoExcelDespesaUseCase : IGeraArquivoExcelDespesaUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IRepositorioDespesaSomenteLeitura _repositorio;
    private readonly ILoggedUser _loggedUser;
    public GeraArquivoExcelDespesaUseCase(IRepositorioDespesaSomenteLeitura repositorio, ILoggedUser loggedUser)
    {
        _repositorio = repositorio;
        _loggedUser = loggedUser;
    }

    public async Task<byte[]> GeraArquivo(DateOnly mes)
    {
        var loggedUser = await _loggedUser.Get();
        var despesas = await _repositorio.FiltraPorMes(loggedUser, mes);

        if (despesas.Count == 0)
        {
            return [];
        }

        using var workbook = new XLWorkbook();
        workbook.Author = loggedUser.Nome;
        workbook.Style.Font.FontSize = 12;

        var planilha = workbook.Worksheets.Add(mes.ToString("Y"));
        InserirCabecalho(planilha);
        var linha = 2;

        foreach (var despesa in despesas)
        {
            planilha.Cell($"A{linha}").Value = despesa.NomeDespesa;
            planilha.Cell($"B{linha}").Value = despesa.DataDespesa;
            planilha.Cell($"C{linha}").Value = ConverteMetodoPagamento(despesa.MetodoPagamento);
            planilha.Cell($"D{linha}").Value = despesa.ValorDespesa;
            planilha.Cell($"D{linha}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #,##0.00";
            planilha.Cell($"E{linha}").Value = despesa.Descricao;
            linha++;
        }
        planilha.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private string ConverteMetodoPagamento(MetodoPagamento pagamento)
    {
        return pagamento switch
        {
            MetodoPagamento.Pix => "Pix",
            MetodoPagamento.Cartao => "Cartão",
            MetodoPagamento.Boleto => "Boleto",
            _ => string.Empty
        };
    }

    private void InserirCabecalho(IXLWorksheet planilha)
    {
        planilha.Cell("A1").Value = ResourceReportGenerationMessages.TITULO;
        planilha.Cell("B1").Value = ResourceReportGenerationMessages.DATA;
        planilha.Cell("C1").Value = ResourceReportGenerationMessages.TIPO_PAGAMENTO;
        planilha.Cell("D1").Value = ResourceReportGenerationMessages.VALOR;
        planilha.Cell("E1").Value = ResourceReportGenerationMessages.DESCRICAO;

        planilha.Cells("A1:E1").Style.Font.Bold = true;
        planilha.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

        planilha.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        planilha.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        planilha.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        planilha.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        planilha.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
