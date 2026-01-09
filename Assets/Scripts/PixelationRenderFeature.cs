using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

public class PixelationRenderFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class PixelationSettings
    {
        public Material pixelationMaterial;
    }
    public PixelationSettings settings;

    class PixelationRenderPass : ScriptableRenderPass
    {
        public Material material;

        public PixelationRenderPass(Material mat)
        {
            material = mat;
        }

        class PixelationPassData
        {
            public TextureHandle source;
            public Material material;
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameContext)
        {
            var cameraData = frameContext.Get<UniversalCameraData>();
            var frameData = frameContext.Get<UniversalResourceData>();

            using var builder = renderGraph.AddRasterRenderPass<PixelationPassData>("PixelationPass", out var passData);

            passData.source = frameData.activeColorTexture;

            TextureDesc destDesc = frameData.activeColorTexture.GetDescriptor(renderGraph);
            TextureHandle dest = renderGraph.CreateTexture(destDesc);

            passData.material = material;

            builder.UseTexture(passData.source);
            builder.SetRenderAttachment(dest, 0);

            builder.SetRenderFunc((PixelationPassData data, RasterGraphContext ctx) =>
            {
                Blitter.BlitTexture(
                    ctx.cmd,
                    data.source,
                    new Vector4(1, 1, 0, 0),
                    data.material,
                    0
                );
            });

            frameData.cameraColor = dest;
        }
    }

    PixelationRenderPass pass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (pass == null) return;

        if (renderingData.cameraData.cameraType == CameraType.Game)
            renderer.EnqueuePass(pass);
    }

    public override void Create()
    {
        if (settings == null) return;
        if (settings.pixelationMaterial == null) return;

        pass = new PixelationRenderPass(settings.pixelationMaterial)
        {
            renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing
        };
    }
}
