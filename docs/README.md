# 《老人与海》Unity Web 游戏全案设计与资产策划索引库 (Master Game Design Docs)

> 本文档库严格依据项目根目录 `docs/oldmansea.pdf`（欧内斯特·海明威原著完整文本）提炼构建，为 Unity Web 游戏项目提供从视觉资产生成、AI 对话提示词、开放海域自由探索、关卡流程编排到物理力学数值的落地技术全案。

---

## 📚 文档矩阵概览

| 模块文件 | 核心内容与职能 | 适用人群/工具链 |
| :--- | :--- | :--- |
| [**04_OPEN_WORLD_EXPLORATION_DESIGN.md**](./04_OPEN_WORLD_EXPLORATION_DESIGN.md) | **【核心架构】开放海域自由探索与无UI航海系统 (Open-Sea GDD)**<br>• $8 \times 8\text{km}$ 连续无缝开放海域架构（安全港 ➔ 大陆架浅滩 ➔ 墨西哥湾流大井 ➔ 远洋深渊禁区）<br>• 无UI写实环境辨识（哈瓦那夜空光晕、猎户座星带与参宿七、海鸟俯冲追踪、积雨云辨别陆地）<br>• 4层深度自由垂直布饵（40/75/100/125寻）与绿树枝触碰预警<br>• 自由漫游到“大鱼全图拖曳”无缝状态切换力学<br>• 16英尺小艇甲板微观自由交互（切生肉、盐水浸手、船上打盹）与海面动态事件池（水母群、疲倦林莺、飞鱼、暴风海风） | 开放世界架构师、系统策划、主程序员 |
| [**01_PROMPT_LIBRARY.md**](./01_PROMPT_LIBRARY.md) | **大鱼与老人 AI 提示词与资产库**<br>• 老人圣地亚哥与大马林鱼外貌设定、2D/3D概念图Prompt（Midjourney v6/SDXL）<br>• Unity PBR材质与次表面散射 (SSS) Shader规范<br>• 角色 Animator 动作状态机定义<br>• Unity LLM 端侧/API 心理独白系统 System Prompt<br>• 鲨鱼敌人 (Mako / Galano) 概念与 AI 状态机 | 美术设计、3D建模师、AI生成工作流、技术美术 (TA) |
| [**02_STORYLINE_AND_LEVEL_PROGRESSION.md**](./02_STORYLINE_AND_LEVEL_PROGRESSION.md) | **全主线叙事与关卡推进设计方案**<br>• 7幕式核心主线脉络（第0幕序章 ~ 第6幕尾声）<br>• 84日厄运破除、百寻深渊咬钩、无声深海拖曳、巨兽破水腾跃、生死回旋处决、四波鲨鱼攻防、未败残骸归港<br>• 关键剧场演出、QTE判定与情感心流曲线 | 游戏主策划、关卡设计师、叙事编剧 |
| [**03_GAME_SYSTEMS_AND_MECHANICS.md**](./03_GAME_SYSTEMS_AND_MECHANICS.md) | **游戏系统机制、道具生态与台词全鉴**<br>• 深海钓线张力物理学（Tension & Friction）<br>• 老人四维生存体征（体力/水分/手伤/神志）与左手痉挛（Calambre）自愈小游戏<br>• 意志力与记忆殿堂（卡萨布兰卡掰手腕、迪马吉奥骨刺、海滩幼狮）<br>• 武器阶梯式崩毁树（鱼叉 ➔ 绑刀桨 ➔ 短木棒 ➔ 破舵柄 ➔ 残刺）<br>• 10种海洋生态生物交互全鉴<br>• 全书经典中英双语原著金句文库（带触发标签） | Unity 逻辑程序员、数值策划、音效设计 (SFX/VO) |

---

## 🎮 开放探索核心玩法循环 (Open-Sea Game Loop)

```mermaid
graph TD
    A[避风港整备: 采购沙丁鱼/带水壶/修残帆] --> B[自由启航: 依据海鸟/洋流深浅深入墨西哥湾流]
    B --> C[浅海探钓: 手线捕集金枪鱼/沙丁鱼作为深渊重饵]
    C --> D[驶入墨黑大井区: 在四层深度垂直布设钓绳 40-125寻]
    D --> E{搜寻/等待咬钩事件}
    E -->|普通生态鱼| F[捕获鲯鳅/飞鱼: 生食补充体力或制成新饵]
    E -->|百寻深渊绿枝下弯| G[巨型马林鱼咬钩: 开启全图无缝动态拖曳生存态]
    F --> D
    G --> H[数日拉锯 ➔ 巨鱼腾跃 ➔ 鱼叉绝杀 ➔ 巨鱼绑舷]
    H --> I[血染海域: 全图鲨群动态追猎大逃杀]
    I -->|武器逐级碎裂| J[肉尽骨存: 望哈瓦那夜光与猎户座星光破晓归港]
    J --> K[不败精神升华: 终章狮子之梦]
```

---

## 🛠️ Unity WebGL 开放海域工程落地建议

1. **WebGL 性能与包体优化**：
   - 海面采用 **Projected Grid + Gerstner Wave Shader**，单水体渲染开销仅 1 个 Draw Call，避免生成巨大水面网格开销。
   - 角色与马林鱼模型推荐使用 Stylized Realistic（风格化写实）风格，单模型面数控制在 15,000 面以内。
2. **AI 与动态叙事驱动**：
   - 文档 `01_PROMPT_LIBRARY.md` 中提供的 LLM System Prompt 可直接对接云端小模型或端侧 WebLLM（如 SmolLM / Llama-3.2-1B-Instruct），实现老人依据实时拉力、体力、天气与迷航状态生成的动态心理独白。
3. **数据驱动与本地化**：
   - 文档 `03_GAME_SYSTEMS_AND_MECHANICS.md` 中的中英双语台词库结构可直接转为 ScriptableObject 或 JSON 资产文件，挂载至游戏 DialogueManager 中。
