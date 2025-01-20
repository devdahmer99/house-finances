using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;
using System.Reflection;

namespace financesFlow.Aplicacao.useCase.Arquivo.Pdf.Fonts;
public class ResolverFontsDespesas : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = LerArquivoFonte(faceName);
        stream ??= LerArquivoFonte(FontHelper.DEFAULT_FONT);
  
        var length = (int)stream!.Length;
        var data = new byte[length];

        stream.Read(buffer: data, offset: 0, count: length);
        return data;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        new Font
        {
            Name = FontHelper.RALEWAY_REGULAR,
        };

        return new FontResolverInfo(familyName, bold, italic);
    }

    private Stream? LerArquivoFonte(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"financesFlow.Aplicacao.useCase.Arquivo.Pdf.Fonts.{faceName}.ttf");
    }
}
