# 3D 资产规格说明书：生锈铁飞叉与长麻索 (Iron Harpoon & Rope)

> **英文标识 (ID)**: `iron-harpoon`  
> **资产层级**: `items/iron-harpoon`  
> **标准规格**: 全长 2.4m, 叉头 0.35m  

---

## 1. 原著设定与生理细节
老旧手工锻造单倒钩铁矛头，装配在坚固硬木白蜡木杆上，铜皮缠绕固紧，尾部系有一圈圈整齐排列的浸焦油厚麻绳。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D weapon prop of a vintage hand-forged iron whaling harpoon. Dark pitted iron spearhead with a sharp single barbed point, mounted onto a sturdy ash-wood pole with copper wrapping. A coil of thick tarred hemp rope attached to the base. 750 triangles, PBR metallic and rust textures.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
weapon turnaround sheet of a vintage iron harpoon, single barb point, copper wrapping, ash wood shaft, coiled rope, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 750 |
| **骨骼与绑定 (Rigging)** | 静态 (绳索带 LineRenderer 物理) |
| **贴图槽位 (PBR Maps)** | 512x512 PBR (生锈铁、粗木、铜箍) |
| **物理碰撞体 (Colliders)** | Capsule Collider |
| **挂点与交互节点 (Sockets)** | Grip_Hand_1, Grip_Hand_2, Penetration_Point |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/items/iron-harpoon/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
