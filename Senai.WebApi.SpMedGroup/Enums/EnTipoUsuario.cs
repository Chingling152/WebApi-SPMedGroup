namespace Senai.WebApi.SpMedGroup.Enums {
    public enum EnTipoUsuario {
        /// <summary>
        /// Paciente contem apenas privilegios de visualização de suas proprias consultas
        /// </summary>
        Paciente = 1,
        /// <summary>
        /// Medico poderá apenas visualizar e alterar suas consultas 
        /// </summary>
        Medico = 2,
        /// <summary>
        /// Administrador é o usuario que contem todos os privilegios
        /// </summary>
        Administrador = 100
    }
}
