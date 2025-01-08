using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Office.PowerPoint.Y2021.M06.Main;
using financesFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;

namespace financesFlow.Aplicacao.useCase.Usuarios
{
    public class ValidacaoSenha<T> : PropertyValidator<T, string>
    {
        private const string MENSAGEM_DE_ERRO = "MensagemErro";
        public override string Name => "ValidacaoSenha";
        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return $"{{{MENSAGEM_DE_ERRO}}}";
        }

        public override bool IsValid(ValidationContext<T> context, string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                context.MessageFormatter.AppendArgument(MENSAGEM_DE_ERRO, ResourceErrorMessages.USUARIO_COM_SENHA_INVALIDA);
                return false;
            }

            if (senha.Length < 8) 
            {
                context.MessageFormatter.AppendArgument(MENSAGEM_DE_ERRO, ResourceErrorMessages.USUARIO_COM_SENHA_INVALIDA);
                return false;
            }

            if (Regex.IsMatch(senha, pattern: @"[A-Z]+") == false)
            {
                context.MessageFormatter.AppendArgument(MENSAGEM_DE_ERRO, ResourceErrorMessages.USUARIO_COM_SENHA_INVALIDA);
                return false;
            }

            if (Regex.IsMatch(senha, pattern: @"[a-z]+") == false)
            {
                context.MessageFormatter.AppendArgument(MENSAGEM_DE_ERRO, ResourceErrorMessages.USUARIO_COM_SENHA_INVALIDA);
                return false;
            }

            if (Regex.IsMatch(senha, pattern: @"[0-9]+") == false)
            {
                context.MessageFormatter.AppendArgument(MENSAGEM_DE_ERRO, ResourceErrorMessages.USUARIO_COM_SENHA_INVALIDA);
                return false;
            }

            if(Regex.IsMatch(senha, pattern: @"[\!\?\*\.]+") == false)
            {
                context.MessageFormatter.AppendArgument(MENSAGEM_DE_ERRO, ResourceErrorMessages.USUARIO_COM_SENHA_INVALIDA);
                return false;
            }

            return true;
        }
    }
}
