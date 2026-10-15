"use client";

import React, { useState, useEffect } from "react";
import { Unity, useUnityContext } from "react-unity-webgl";

export default function GameCanvas() {
  const [mode, setMode] = useState<"canvas" | "iframe">("canvas");
  const [mounted, setMounted] = useState(false);

  useEffect(() => {
    setMounted(true);
  }, []);

  const {
    unityProvider,
    isLoaded,
    loadingProgression,
    requestFullscreen,
    initialisationError,
  } = useUnityContext({
    loaderUrl: "/unity-build/Build/unity-build.loader.js",
    dataUrl: "/unity-build/Build/unity-build.data",
    frameworkUrl: "/unity-build/Build/unity-build.framework.js",
    codeUrl: "/unity-build/Build/unity-build.wasm",
  });

  if (!mounted) {
    return (
      <div className="w-full aspect-[16/10] bg-slate-900 rounded-2xl flex items-center justify-center text-slate-400">
        正在初始化海域环境...
      </div>
    );
  }

  return (
    <div className="relative w-full flex flex-col bg-slate-950 rounded-2xl border border-slate-800 shadow-2xl overflow-hidden">
      {/* 顶部控制栏 */}
      <div className="w-full px-5 py-3.5 bg-slate-900/95 border-b border-slate-800 flex flex-wrap items-center justify-between gap-3 text-sm">
        <div className="flex items-center gap-3">
          <span className="relative flex h-3 w-3">
            <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-emerald-400 opacity-75"></span>
            <span className="relative inline-flex rounded-full h-3 w-3 bg-emerald-500"></span>
          </span>
          <div className="flex items-center gap-2">
            <span className="font-semibold text-slate-100 tracking-wide">
              墨西哥湾流 · 实时交互海域
            </span>
            <span className="text-xs px-2 py-0.5 rounded-full bg-slate-800 text-amber-300 border border-amber-900/40">
              Unity 6 WebGL
            </span>
          </div>
        </div>

        <div className="flex items-center gap-2.5">
          <button
            onClick={() => setMode(mode === "canvas" ? "iframe" : "canvas")}
            className="px-3 py-1.5 text-xs rounded-lg bg-slate-800 hover:bg-slate-700 text-slate-200 transition border border-slate-700"
            title="在 React 内嵌组件与独立沙箱 iframe 间切换"
          >
            {mode === "canvas" ? "切换至独立沙箱模式" : "切换至 React 组件模式"}
          </button>

          <a
            href="/unity-build/index.html"
            target="_blank"
            rel="noopener noreferrer"
            className="px-3 py-1.5 text-xs rounded-lg bg-cyan-950/80 hover:bg-cyan-900 text-cyan-300 border border-cyan-800/60 transition flex items-center gap-1"
          >
            新窗口独立运行 ↗
          </a>

          {mode === "canvas" && (
            <button
              onClick={() => requestFullscreen(true)}
              disabled={!isLoaded}
              className="px-3.5 py-1.5 text-xs rounded-lg bg-gradient-to-r from-amber-600 to-amber-500 hover:from-amber-500 hover:to-amber-400 disabled:opacity-40 text-slate-950 font-semibold shadow-md transition"
            >
              ⛶ 全屏游玩
            </button>
          )}
        </div>
      </div>

      {/* 游戏视图容器 */}
      <div className="relative w-full aspect-[16/10] min-h-[500px] max-h-[720px] bg-slate-950 flex items-center justify-center select-none">
        {mode === "iframe" ? (
          <iframe
            src="/unity-build/index.html"
            className="w-full h-full border-0"
            title="老人与海 Web 游戏"
            allow="fullscreen; autoplay; keyboard"
          />
        ) : (
          <>
            {/* 加载中动画与提示 */}
            {!isLoaded && !initialisationError && (
              <div className="absolute inset-0 z-20 flex flex-col items-center justify-center bg-slate-950/95 text-center p-8 backdrop-blur-sm">
                <div className="max-w-md flex flex-col items-center">
                  <div className="text-amber-300 font-serif text-lg tracking-wider mb-2">
                    “一个人可以被毁灭，但不能被打败。”
                  </div>
                  <div className="text-slate-400 text-xs mb-6">
                    正在装载 3D 洋流仿真、18英尺马林鱼物理拖拽、四维生存体征系统...
                  </div>

                  <div className="w-80 h-2 bg-slate-800 rounded-full overflow-hidden border border-slate-700/80">
                    <div
                      className="h-full bg-gradient-to-r from-amber-500 via-amber-400 to-cyan-400 transition-all duration-300 ease-out"
                      style={{ width: `${Math.round(loadingProgression * 100)}%` }}
                    />
                  </div>

                  <div className="mt-3 text-xs text-amber-400 font-mono">
                    资源加载中 {Math.round(loadingProgression * 100)}%
                  </div>
                </div>
              </div>
            )}

            {initialisationError && (
              <div className="absolute inset-0 z-20 flex flex-col items-center justify-center bg-red-950/90 text-center p-6 text-red-200">
                <div className="font-bold text-lg mb-2">WebGL 加载遇到异常</div>
                <div className="text-xs max-w-lg mb-4 text-red-300 font-mono">
                  {initialisationError.message}
                </div>
                <button
                  onClick={() => setMode("iframe")}
                  className="px-4 py-2 bg-red-800 text-white text-xs rounded-lg hover:bg-red-700 transition"
                >
                  尝试使用沙箱模式运行
                </button>
              </div>
            )}

            <Unity
              unityProvider={unityProvider}
              className="w-full h-full object-contain focus:outline-none"
              tabIndex={1}
            />
          </>
        )}
      </div>
    </div>
  );
}
