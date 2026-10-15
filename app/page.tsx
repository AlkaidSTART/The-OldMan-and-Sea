import GameCanvas from "./components/GameCanvas";

export default function Home() {
  return (
    <div className="min-h-screen bg-gradient-to-b from-slate-950 via-[#07131e] to-slate-950 text-slate-100 flex flex-col font-sans">
      {/* 顶部导航 */}
      <header className="border-b border-slate-800/80 bg-slate-950/80 backdrop-blur sticky top-0 z-30">
        <div className="max-w-6xl mx-auto px-6 py-4 flex flex-wrap items-center justify-between gap-3">
          <div className="flex items-center gap-3">
            <span className="text-2xl">🍺</span>
            <div>
              <h1 className="text-lg font-bold text-amber-100 tracking-wide">
                《老人与海》全主线探索体验 · The Old Man and the Sea
              </h1>
              <p className="text-xs text-slate-400">
                原著还原 · 出航前夜露台饭店 ➔ 陋室整备 ➔ 栈桥启航 ➔ 墨西哥湾流自由探索
              </p>
            </div>
          </div>

          <div className="flex items-center gap-2">
            <span className="text-xs px-2.5 py-1 rounded-md bg-amber-950/70 text-amber-300 border border-amber-800/60 font-medium">
              第0幕：84日厄运与露台饭店
            </span>
            <span className="text-xs px-2.5 py-1 rounded-md bg-cyan-950/70 text-cyan-300 border border-cyan-800/60 font-medium">
              无缝开放海域探索
            </span>
          </div>
        </div>
      </header>

      {/* 主体体验区 */}
      <main className="flex-1 max-w-6xl w-full mx-auto px-6 py-8 flex flex-col gap-8">
        {/* 剧情阶段导航导引 */}
        <section className="w-full bg-slate-900/60 border border-slate-800 rounded-2xl p-4 flex flex-col gap-3">
          <div className="text-xs font-semibold text-amber-300 flex items-center gap-2">
            <span>📖</span> 原著主线剧情演进路线 (Story Progression Timeline)
          </div>
          <div className="grid grid-cols-1 md:grid-cols-4 gap-3 text-xs">
            <div className="p-3 rounded-xl bg-slate-950/80 border border-amber-500/40 flex flex-col gap-1">
              <div className="font-bold text-amber-300 flex items-center gap-1.5">
                <span>🍺</span> 1. 露台饭店喝啤酒
              </div>
              <p className="text-slate-400 text-[11px] leading-relaxed">
                在科希马尔海滩漫步，前往露台饭店与忠诚男孩马诺林相聚，喝下清凉啤酒，谈论洋基队与坚韧的迪马吉奥。
              </p>
            </div>

            <div className="p-3 rounded-xl bg-slate-950/80 border border-slate-800 flex flex-col gap-1">
              <div className="font-bold text-slate-300 flex items-center gap-1.5">
                <span>🏠</span> 2. 木棚屋陋室整备
              </div>
              <p className="text-slate-400 text-[11px] leading-relaxed">
                走回简陋木棚屋，喝铝罐煮出的滚烫苦咖啡，检视墙上圣母像、弹簧床上的旧报纸，装满淡水瓶。
              </p>
            </div>

            <div className="p-3 rounded-xl bg-slate-950/80 border border-slate-800 flex flex-col gap-1">
              <div className="font-bold text-slate-300 flex items-center gap-1.5">
                <span>⛵</span> 3. 清晨栈桥启航
              </div>
              <p className="text-slate-400 text-[11px] leading-relaxed">
                男孩送来两包鲜沙丁鱼重饵，合力搬运渔具登船。在黑夜与晨星中推船入水，告别男孩，划向茫茫大海。
              </p>
            </div>

            <div className="p-3 rounded-xl bg-slate-950/80 border border-slate-800 flex flex-col gap-1">
              <div className="font-bold text-slate-300 flex items-center gap-1.5">
                <span>🎣</span> 4. 开放海域与百寻巨兽
              </div>
              <p className="text-slate-400 text-[11px] leading-relaxed">
                自由划桨穿越浅滩驶入墨西哥湾流大井区，在40/75/100/125寻精确布饵，静待百寻深处绿树枝剧烈下弯！
              </p>
            </div>
          </div>
        </section>

        {/* 游戏画布 */}
        <section className="w-full">
          <GameCanvas />
        </section>

        {/* 详细操作指南与机制卡片 */}
        <section className="grid grid-cols-1 md:grid-cols-3 gap-6">
          {/* 陆地与航海操作 */}
          <div className="bg-slate-900/60 border border-slate-800 rounded-xl p-5 flex flex-col gap-3">
            <div className="flex items-center gap-2 text-amber-400 font-semibold text-sm">
              <span>🎮</span> 核心交互按键 (Controls)
            </div>
            <ul className="text-xs text-slate-300 flex flex-col gap-2">
              <li className="flex items-center justify-between bg-slate-950/60 p-2 rounded-lg border border-slate-800/60">
                <span>陆地漫步 / 小船划桨与转向</span>
                <span className="font-mono bg-slate-800 px-2 py-0.5 rounded text-amber-300 font-bold">W / A / S / D</span>
              </li>
              <li className="flex items-center justify-between bg-slate-950/60 p-2 rounded-lg border border-slate-800/60">
                <span>与地标交互 / 推进对话</span>
                <span className="font-mono bg-slate-800 px-2 py-0.5 rounded text-emerald-300 font-bold">E 键 / F 键</span>
              </li>
              <li className="flex items-center justify-between bg-slate-950/60 p-2 rounded-lg border border-slate-800/60">
                <span>对话翻页 / 诱引大鱼咬钩</span>
                <span className="font-mono bg-slate-800 px-2 py-0.5 rounded text-amber-300 font-bold">[空格键]</span>
              </li>
              <li className="flex items-center justify-between bg-slate-950/60 p-2 rounded-lg border border-slate-800/60">
                <span>开放海域 4 层深度布饵</span>
                <span className="font-mono bg-slate-800 px-2 py-0.5 rounded text-cyan-300 font-bold">1 / 2 / 3 / 4 键</span>
              </li>
              <li className="flex items-center justify-between bg-slate-950/60 p-2 rounded-lg border border-slate-800/60">
                <span>搏杀拉锯：锁线收线</span>
                <span className="font-mono bg-slate-800 px-2 py-0.5 rounded text-rose-300 font-bold">长按 [空格键]</span>
              </li>
              <li className="flex items-center justify-between bg-slate-950/60 p-2 rounded-lg border border-slate-800/60">
                <span>生存体征：水瓶饮水 / 解痉挛</span>
                <span className="font-mono bg-slate-800 px-2 py-0.5 rounded text-slate-300">E 饮水 | 长按 Q 搓揉</span>
              </li>
            </ul>
          </div>

          {/* 探索场景与剧情锚点 */}
          <div className="bg-slate-900/60 border border-slate-800 rounded-xl p-5 flex flex-col gap-3">
            <div className="flex items-center gap-2 text-cyan-400 font-semibold text-sm">
              <span>📍</span> 场景地标与原著剧情锚点
            </div>
            <div className="text-xs text-slate-300 flex flex-col gap-2.5">
              <div className="p-2 rounded-lg bg-slate-950/60 border border-slate-800/60">
                <span className="font-semibold text-amber-300">🍺 露台饭店 (The Terrace)</span>
                <p className="text-slate-400 mt-0.5">
                  位于沙滩西侧木质遮阳棚下。走近桌旁按 <kbd className="text-amber-300 font-mono">E/F</kbd>，触发喝啤酒、男孩宽慰与棒球对话。
                </p>
              </div>

              <div className="p-2 rounded-lg bg-slate-950/60 border border-slate-800/60">
                <span className="font-semibold text-cyan-300">🏠 圣地亚哥木棚屋 (Shack)</span>
                <p className="text-slate-400 mt-0.5">
                  位于东侧山坡下的简陋棕榈木屋。走近按 <kbd className="text-cyan-300 font-mono">E/F</kbd> 检视旧报纸、喝苦咖啡与整理水壶。
                </p>
              </div>

              <div className="p-2 rounded-lg bg-slate-950/60 border border-slate-800/60">
                <span className="font-semibold text-emerald-300">⛵ 栈桥与16英尺小船 (Pier & Skiff)</span>
                <p className="text-slate-400 mt-0.5">
                  停泊在中央栈桥尽头的海水中。走近按 <kbd className="text-emerald-300 font-mono">E/F</kbd> 登船推入深海，进入自由划桨航海！
                </p>
              </div>
            </div>
          </div>

          {/* 开放海域探索水文 */}
          <div className="bg-slate-900/60 border border-slate-800 rounded-xl p-5 flex flex-col gap-3">
            <div className="flex items-center gap-2 text-emerald-400 font-semibold text-sm">
              <span>🌊</span> 开放海域分级水文与生态
            </div>
            <div className="text-xs text-slate-300 flex flex-col gap-2.5">
              <div className="p-2 rounded-lg bg-emerald-950/40 border border-emerald-900/50">
                <span className="font-bold text-emerald-300">Zone 0 · 科希马尔避风港 (0~50米)</span>
                <p className="text-slate-400 mt-0.5">
                  内湾平缓碧波，回望可见渔村点点煤油灯微光与岸边码头。
                </p>
              </div>

              <div className="p-2 rounded-lg bg-amber-950/40 border border-amber-900/50">
                <span className="font-bold text-amber-300">Zone 1 · 大陆架浅海绿区 (50~140米)</span>
                <p className="text-slate-400 mt-0.5">
                  翡翠绿海水，飞鱼破浪跃迁，大片马尾藻漂浮，适宜下40/75寻浅线。
                </p>
              </div>

              <div className="p-2 rounded-lg bg-blue-950/40 border border-blue-900/50">
                <span className="font-bold text-blue-300">Zone 2 · 墨西哥湾流大井区 (140米+)</span>
                <p className="text-slate-400 mt-0.5">
                  水色骤降为幽邃纯黑靛蓝，涌浪变长。在此布下100寻金枪鱼重饵，引爆千磅巨兽咬钩！
                </p>
              </div>
            </div>
          </div>
        </section>

        {/* 原著金句 */}
        <section className="bg-slate-900/30 border border-slate-800/80 rounded-xl p-6 flex flex-col sm:flex-row items-center justify-between gap-4">
          <div className="flex flex-col gap-1">
            <blockquote className="text-amber-200/90 font-serif italic text-sm">
              “他不再梦见风暴，不再梦见妇女，不再梦见伟大的事件，不再梦见大鱼，也不再梦见打架、比力气或他的妻子。他现在只梦见各个地方，和海滩上的那些狮子。”
            </blockquote>
            <p className="text-xs text-slate-400">
              —— 欧内斯特·海明威《老人与海》（Ernest Hemingway）
            </p>
          </div>
          <a
            href="/unity-build/index.html"
            target="_blank"
            rel="noopener noreferrer"
            className="shrink-0 px-4 py-2 text-xs font-semibold rounded-lg bg-slate-800 hover:bg-slate-700 text-slate-200 border border-slate-700 transition"
          >
            新标签独立全屏运行 ↗
          </a>
        </section>
      </main>

      {/* 页脚 */}
      <footer className="border-t border-slate-800/80 py-4 text-center text-xs text-slate-400 bg-slate-950">
        《老人与海》开放世界剧情探索工程 · Next.js 16 Web 交互端
      </footer>
    </div>
  );
}
