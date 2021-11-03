using System;

namespace VerificadorXml.Models
{
    public class Cadastro
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ServerImap { get; set; }
        public bool Ativo { get; set; }
    }
}
