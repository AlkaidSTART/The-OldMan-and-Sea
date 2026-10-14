# 3D 资产规格说明书：疲倦的林莺 (The Tired Warbler)

> **英文标识 (ID)**: `warbler`  
> **资产层级**: `creatures/warbler`  
> **标准规格**: 体长 0.12m, 娇小鸟禽  

---

## 1. 原著设定与生理细节
跨洋迁徙疲惫不堪的小候鸟，短暂停留在老人绷直的沉重钓线上歇息，随后在大鱼猛拉下飞离。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D tiny songbird model, yellow and olive-green warbler, perched pose with claws gripping a cord, ruffled exhausted feathers, low poly, 500 triangles.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
turnaround sheet of small yellow warbler bird, perched and flight poses, clean white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 500 |
| **骨骼与绑定 (Rigging)** | 6 根骨骼 |
| **贴图槽位 (PBR Maps)** | 256x256 PBR |
| **物理碰撞体 (Colliders)** | Sphere Collider (Trigger) |
| **挂点与交互节点 (Sockets)** | Claw_Grip_Point |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/creatures/warbler/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
