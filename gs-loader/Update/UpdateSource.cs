using System;

namespace gs_loader.Update
{
    /// <summary>
    /// Classe para identificar os dados de origem para atualização
    /// </summary>
    public class UpdateSource : ICloneable
    {
        /// <summary>
        /// Tipo da origem
        /// </summary>
        public UpdateSourceType Type { get; set; } = UpdateSourceType.Folder;

        /// <summary>
        /// Endereço: Pasta, Endereço FTP, ou Endereço HTTP
        /// </summary>
        public string Address { get; set; } = "";

        /// <summary>
        /// Usuário para acesso ao endereço
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Password { get; set; } = "";

        public object Clone() => new UpdateSource
        {
            Type = Type,
            Address = Address,
            UserName = UserName,
            Password = Password
        };

    }
}
