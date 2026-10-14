# 3D 资产规格说明书：露台饭店女游客 (The Tourist)

> **英文标识 (ID)**: `tourist`  
> **资产层级**: `characters/tourist`  
> **标准规格**: 高度 1.68m, 1950年代上流社会装扮  

---

## 1. 原著设定与生理细节
在海边露台饭店喝啤酒、看着漂浮白骨把大马林鱼误认成鲨鱼的女游客，象征外界对硬汉抗争历史的戏剧性误解与冷漠。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
Low-poly 1950s vintage female tourist NPC, wearing a pale yellow summer cocktail dress, cat-eye sunglasses, wide-brim sunhat, carrying a small clutch, elegant pose, clean topology, under 3000 triangles, Unity WebGL ready.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
character turnaround sheet, 1950s stylish female tourist, yellow dress, cat-eye glasses, white background, orthographic view. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 2,500 - 3,000 |
| **骨骼与绑定 (Rigging)** | 45 根骨骼 |
| **贴图槽位 (PBR Maps)** | 512x512 PBR |
| **物理碰撞体 (Colliders)** | Capsule Collider |
| **挂点与交互节点 (Sockets)** | Hand_Clutch |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/characters/tourist/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
