using System.ComponentModel.DataAnnotations;

namespace Nameless.Infra.Resources
{
    public enum Language
    {
        [Display(Description = "en")]
        EN = 0,

        [Display(Description = "pt-BR")]
        PT_BR = 1
    }
}