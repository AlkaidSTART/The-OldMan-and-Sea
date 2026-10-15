import GameCanvas from "./components/GameCanvas";

export default function Home() {
  return (
    <div className="min-h-screen bg-gradient-to-b from-slate-950 via-[#07131e] to-slate-950 text-slate-100 flex flex-col font-sans">
      {/* 顶部导航 */}
      <header className="border-b border-slate-800/80 bg-slate-950/70 backdrop-blur sticky top-0 z-30">
        <div className="max-w-6xl mx-auto px-6 py-4 flex items-center justify-between">
          <div className="flex items-center gap-3">
            <span className="text-2xl">🎣</span>
            <div>
              <h1 className="text-lg font-bold text-amber-100 tracking-wide">
                《老人与海》The Old Man and the Sea
              </h1>
              <p className="text-xs text-slate-400">
                Unity 6 WebGL 原型 · 3D 开放海域探索与意志力博弈
              </p>
            </div>
          </div>

          <div className="flex items-center gap-2">
            <span className="text-xs px-2.5 py-1 rounded-md bg-blue-950/70 text-blue-300 border border-blue-800/60">
              墨西哥湾流 84天
            </span>
            <span className="text-xs px-2.5 py-1 rounded-md bg-amber-950/70 text-amber-300 border border-amber-800/60">
              18英尺大马林鱼
            </span>
          </div>
        </div>
      </header>

      {/* 主体游戏区 */}
      <main className="flex-1 max-w-6xl w-full mx-auto px-6 py-8 flex flex-col gap-8">
        {/* 游戏画布 */}
        <section className="w-full">
          <GameCanvas />
        </section>

        {/* 玩法指引与核心机制卡片 */}
        <section className="grid grid-cols-1 md:grid-cols-3 gap-6">
          {/* 操作按键指南 */}
          <div className="bg-slate-900/60 border border-slate-800 rounded-xl p-5 flex flex-col gap-3">
            <div className="flex items-center gap-2 text-amber-400 font-semibold text-sm">
              <span>🎮</span> 核心操作指南 (Controls)
            </div>
            <ul className="text-xs text-slate-300 flex flex-col gap-2.5">
              <li className="flex items-center justify-between bg-slate-950/60 p-2 rounded-lg border border-slate-800/60">
                <span>划木桨 / 小艇航行转向</span>
                <span className="font-mono bg-slate-800 px-2 py-0.5 rounded text-amber-300 font-bold">W / A / S / D</span>
              </li>
              <li className="flex items-center justify-between bg-slate-950/60 p-2 rounded-lg border border-slate-800/60">
                <span>压阻尼锁线收线 (Reel In)</span>
                <span className="font-mono bg-slate-800 px-2 py-0.5 rounded text-amber-300 font-bold">长按 [空格键]</span>
              </li>
              <li className="flex items-center justify-between bg-slate-950/60 p-2 rounded-lg border border-slate-800/60">
                <span>顺流让线泄力 (Give Slack)</span>
                <span className="font-mono bg-slate-800 px-2 py-0.5 rounded text-slate-400">松开 [空格键]</span>
              </li>
              <li className="flex items-center justify-between bg-slate-950/60 p-2 rounded-lg border border-slate-800/60">
                <span>棕色玻璃水瓶饮水 (解渴)</span>
                <span className="font-mono bg-slate-800 px-2 py-0.5 rounded text-cyan-300 font-bold">E 键</span>
              </li>
              <li className="flex items-center justify-between bg-slate-950/60 p-2 rounded-lg border border-slate-800/60">
                <span>快速搓揉左手肌肉 (解痉挛)</span>
                <span className="font-mono bg-slate-800 px-2 py-0.5 rounded text-red-300 font-bold">长按 Q 键</span>
              </li>
            </ul>
          </div>

          {/* 四维生存体征 */}
          <div className="bg-slate-900/60 border border-slate-800 rounded-xl p-5 flex flex-col gap-3">
            <div className="flex items-center gap-2 text-cyan-400 font-semibold text-sm">
              <span>🩺</span> 圣地亚哥四维生存体征
            </div>
            <div className="text-xs text-slate-300 flex flex-col gap-3">
              <div>
                <span className="font-semibold text-emerald-400">体力 (Stamina)</span>
                <p className="text-slate-400 mt-0.5">划桨、抵抗拖拽与强力收线均大幅消耗体力。耗尽将无力制衡巨鱼。</p>
              </div>
              <div>
                <span className="font-semibold text-cyan-400">水分 (Hydration)</span>
                <p className="text-slate-400 mt-0.5">加勒比烈日下水分持续流失。请适时按 <kbd className="text-cyan-300 font-mono">E</kbd> 小口抿饮纯净水。</p>
              </div>
              <div>
                <span className="font-semibold text-rose-400">手伤与痉挛 (Hand Injury & Cramp)</span>
                <p className="text-slate-400 mt-0.5">高张力钓线会割破手掌。触发左手痉挛时需按 <kbd className="text-rose-300 font-mono">Q</kbd> 搓揉缓解。</p>
              </div>
              <div>
                <span className="font-semibold text-amber-400">神志 (Clarity)</span>
                <p className="text-slate-400 mt-0.5">长时间对抗巨鱼将出现幻觉与意识模糊，需保持冷静与意志力。</p>
              </div>
            </div>
          </div>

          {/* 钓线力学张力计 */}
          <div className="bg-slate-900/60 border border-slate-800 rounded-xl p-5 flex flex-col gap-3">
            <div className="flex items-center gap-2 text-emerald-400 font-semibold text-sm">
              <span>⚖️</span> 钓线张力 (Tension) 战术
            </div>
            <div className="text-xs text-slate-300 flex flex-col gap-2.5">
              <div className="p-2.5 rounded-lg bg-emerald-950/40 border border-emerald-900/50">
                <span className="font-bold text-emerald-300">30 ~ 75 寻：最优消耗区 (Optimal)</span>
                <p className="text-slate-400 mt-1">
                  保持在此区间可平稳磨损巨鱼体力，同时小船随鱼漂流。
                </p>
              </div>

              <div className="p-2.5 rounded-lg bg-amber-950/40 border border-amber-900/50">
                <span className="font-bold text-amber-300">&lt; 30 寻：虚线松脱危险 (Slack Danger)</span>
                <p className="text-slate-400 mt-1">
                  张力过低会导致钓钩松脱，必须立即长按空格键收紧线缆！
                </p>
              </div>

              <div className="p-2.5 rounded-lg bg-rose-950/40 border border-rose-900/50">
                <span className="font-bold text-rose-300">&gt; 90 寻：崩断绝境 (Snap Danger)</span>
                <p className="text-slate-400 mt-1">
                  钓线即将被巨鱼拉断！必须迅速松开空格键顺势让线泄力！
                </p>
              </div>
            </div>
          </div>
        </section>

        {/* 原著精神背景 */}
        <section className="bg-slate-900/30 border border-slate-800/80 rounded-xl p-6 flex flex-col sm:flex-row items-center justify-between gap-4">
          <div className="flex flex-col gap-1">
            <blockquote className="text-amber-200/90 font-serif italic text-sm">
              “每一天都是一个新的日子。走运当然好，但我宁愿做到分毫不差。这样，运气来的时候，你就有所准备了。”
            </blockquote>
            <p className="text-xs text-slate-400">
              —— 欧内斯特·海明威《老人与海》（Ernest Hemingway, The Old Man and the Sea）
            </p>
          </div>
          <a
            href="/unity-build/index.html"
            target="_blank"
            rel="noopener noreferrer"
            className="shrink-0 px-4 py-2 text-xs font-semibold rounded-lg bg-slate-800 hover:bg-slate-700 text-slate-200 border border-slate-700 transition"
          >
            独立窗口全屏体验 ↗
          </a>
        </section>
      </main>

      {/* 底部信息 */}
      <footer className="border-t border-slate-800/80 py-4 text-center text-xs text-slate-400 bg-slate-950">
        《老人与海》Unity 6 WebGL 原型工程 · Next.js 16 Web 交互端
      </footer>
    </div>
  );
}
