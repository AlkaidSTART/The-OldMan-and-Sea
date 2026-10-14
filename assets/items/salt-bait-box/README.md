# 3D 资产规格说明书：粗盐诱饵木箱 (Salted Bait Crate)

> **英文标识 (ID)**: `salt-bait-box`  
> **资产层级**: `items/salt-bait-box`  
> **标准规格**: 长 0.45m, 宽 0.3m, 高 0.2m  

---

## 1. 原著设定与生理细节
带有合页小木箱，铺满粗白海盐结晶，保存男孩送来的新鲜沙丁鱼，防止热带阳光暴晒腐败。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D prop of a small hinged wooden crate covered in coarse white rock salt crystals, with two salted sardines resting inside, 500 triangles.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
prop sheet of a wooden bait box with salt and sardines, top angled view, open lid, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 480 |
| **骨骼与绑定 (Rigging)** | 1 根 (箱盖掀开铰链) |
| **贴图槽位 (PBR Maps)** | 512x512 PBR |
| **物理碰撞体 (Colliders)** | Box Collider |
| **挂点与交互节点 (Sockets)** | Bait_Extract_Point |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/items/salt-bait-box/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
