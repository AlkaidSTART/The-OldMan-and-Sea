# 3D 资产规格说明书：18英尺马林鱼白骨残骸 (Marlin Skeleton)

> **英文标识 (ID)**: `marlin-skeleton`  
> **资产层级**: `creatures/marlin-skeleton`  
> **标准规格**: 体长 18英尺 (5.5m), 漂浮骨架  

---

## 1. 原著设定与生理细节
肉体被鲨群撕咬殆尽后绑在船舷的雪白骨架，包含完整的吻剑鱼头颅、长白脊柱骨节、根根弯曲的胸骨肋条与末端巨大的暗色新月尾鳍。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D game prop model of a colossal 18-foot marlin skeleton. Pure bleached white spine backbone, rib bones curving down, skull with the long pointed bill sword intact, and the large intact dark crescent tail at the far end. Flesh completely stripped, hollow eye sockets, washed-out driftwood texture with salt crystals, game-ready environment prop, Unity WebGL asset, modular mesh, under 6000 triangles.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
isolated model sheet of an 18-foot giant marlin fish skeleton, side profile and perspective view. Bleached white backbone and ribs, long pointed bill skull, crescent tail fin remaining at the back. Studio lighting, neutral grey background, clean bone geometry reference for 3D modeling. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 6,000 - 8,000 |
| **骨骼与绑定 (Rigging)** | 12 根骨骼 (模拟随海浪柔性起伏) |
| **贴图槽位 (PBR Maps)** | 1024x1024 PBR (漂白骨质高反光与粗糙纹理) |
| **物理碰撞体 (Colliders)** | Mesh Collider (Convex) |
| **挂点与交互节点 (Sockets)** | Hull_Lash_Front, Hull_Lash_Mid, Hull_Lash_Tail (与小艇右舷捆绑节点) |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/creatures/marlin-skeleton/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
