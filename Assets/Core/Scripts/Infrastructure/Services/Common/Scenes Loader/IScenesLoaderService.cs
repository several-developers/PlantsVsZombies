using System;
using Core.Utilities;

namespace Core.Infrastructure.Services.Common
{
    public interface IScenesLoaderService
    {
        event Action OnSceneStartLoading;
        event Action OnSceneFinishedLoading;
        void LoadScene(SceneName sceneName);
    }
}