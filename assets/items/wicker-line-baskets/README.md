# 3D 资产规格说明书：深海钓线柳条篮 (Wicker Line Baskets)

> **英文标识 (ID)**: `wicker-line-baskets`  
> **资产层级**: `items/wicker-line-baskets`  
> **标准规格**: 直径 0.6m, 高 0.25m  

---

## 1. 原著设定与生理细节
编织精密的圆形柳条浅筐，内部盘绕整整两卷浸过绿焦油的坚固深海钓索，保证出线顺滑不打结。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D prop of a round woven wicker basket containing neatly coiled thick green braided fishing cord, 500 triangles, clean PBR normal map.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
prop sheet of a wicker fishing line basket, neatly coiled green cord inside, top and angled view, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 520 |
| **骨骼与绑定 (Rigging)** | 静态 |
| **贴图槽位 (PBR Maps)** | 512x512 PBR (编织柳条与编织线圈法线) |
| **物理碰撞体 (Colliders)** | Cylinder Collider |
| **挂点与交互节点 (Sockets)** | Line_Dispenser_Point |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/items/wicker-line-baskets/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
