using System.ComponentModel;

namespace DESAFIO_PRATICO.API.Enums
{
    public enum StatusTasks
    {
        [Description("To Do")]
        ToDo = 1,

        [Description("In Progress")]
        InProgress = 2,

        [Description("Done")]
        Done = 3
    }
}
