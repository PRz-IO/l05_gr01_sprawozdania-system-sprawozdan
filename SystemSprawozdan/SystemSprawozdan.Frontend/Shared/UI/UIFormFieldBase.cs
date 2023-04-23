using Microsoft.AspNetCore.Components;
using Radzen;

namespace SystemSprawozdan.Frontend.Shared.UI
{
    public class UIFormFieldBase : ComponentBase
    {
        [Parameter]
        public string? Label { get; set; } = "";
        [Parameter]
        public string? Name { get; set; } = "";
        [Parameter]
        public bool Required { get; set; } = false;
        [Parameter]
        public bool Disabled { get; set; } = false;

        public Variant Variant = Variant.Outlined;
    }
}
