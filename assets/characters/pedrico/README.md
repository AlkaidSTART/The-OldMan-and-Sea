# 3D 资产规格说明书：老渔夫佩德里科 (Pedrico)

> **英文标识 (ID)**: `pedrico`  
> **资产层级**: `characters/pedrico`  
> **标准规格**: 高度 1.72m, 粗壮老水手体态  

---

## 1. 原著设定与生理细节
港口热心的老渔民，替老人照看小艇与残存骨架，手持长绳丈量18英尺马林鱼脊柱，获得马林鱼头用作捕鱼陷阱饵料。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
Low-poly 3D NPC model of an older Cuban dock fisherman Pedrico, wearing a battered straw fedora hat, faded blue dungarees, rubber fishing boots, holding a coiled measuring rope, friendly weathered face, Unity WebGL asset, under 3500 triangles.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
character model sheet of an older dock fisherman in dungarees and straw hat, front and side view, neutral lighting, white background, low-poly 3D reference. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 3,000 - 3,500 |
| **骨骼与绑定 (Rigging)** | 45 根标准骨骼 |
| **贴图槽位 (PBR Maps)** | 512x512 PBR |
| **物理碰撞体 (Colliders)** | Capsule Collider |
| **挂点与交互节点 (Sockets)** | Hand_Socket (丈量绳索/砍刀) |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/characters/pedrico/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
