using System.Collections.Generic;
using System.Linq;
using Spectre.Console.Rendering;

namespace FolderTool.Rendering;

internal sealed class Inline : Renderable
{
    public IEnumerable<IRenderable> Components { get; }

    public Inline(IEnumerable<IRenderable> components)
    {
        Components = components;
    }

    public Inline(params IRenderable[] components)
        : this((IEnumerable<IRenderable>)components) { }

    protected override IEnumerable<Segment> Render(RenderContext context, int maxWidth) =>
        Components.SelectMany(component => component.Render(context, maxWidth));
}
