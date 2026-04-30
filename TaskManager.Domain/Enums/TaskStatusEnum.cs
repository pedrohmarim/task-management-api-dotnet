namespace TaskManager.Domain.Enums
{
    public enum TaskStatusEnum
    {
        // Poderia ser utilizado DescriptionAttribute ou um CustomAttribute
        // para mapear valores descritivos (ex: "Pendente", "Em andamento")
        // ou códigos internos (ex: "P", "IP"), acessados via extension methods.
        //
        // Exemplo:
        // TaskStatusEnum.Pending.GetDescription()       → "Pendente"
        // TaskStatusEnum.Pending.GetCustomDescription() → "P"
        //
        // Não apliquei aqui para manter simplicidade,
        // pois não havia necessidade no contexto atual, mas dependendo ajuda bastante :)

        Pending = 1,
        InProgress = 2,
        Done = 3
    }
}