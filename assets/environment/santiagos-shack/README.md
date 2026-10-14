# 3D 资产规格说明书：老人的海边木棚陋室 (Santiago's Coastal Shack)

> **英文标识 (ID)**: `santiagos-shack`  
> **资产层级**: `environment/santiagos-shack`  
> **标准规格**: 长 4.5m, 宽 3.5m, 高 2.8m  

---

## 1. 原著设定与生理细节
由皇家棕榈树苞壳（Guano）与灰色风化木板搭建的单间小茅屋，泥土地面；屋内有铺着旧报纸的破弹簧床、木桌、洗脸水盆、挂在墙上的彩色耶稣圣心像与加布里埃尔圣母像。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D low-poly building interior and exterior of Santiago's humble coastal shack. Walls made of tough budshields of royal palm (guano) and gray weathered timber planks, dirt floor, thatched palm leaf roof. Interior includes a rustic wooden cot with springs covered in old yellowed newspapers, a wobbly small table, a wash basin, and framed faded prints of Sacred Heart of Jesus on the wall. Modular layout, optimized for Unity WebGL, under 5000 triangles total, rustic colonial Cuban palette.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
architectural concept sheet of a humble Cuban coastal fisherman shack, thatched palm roof, weathered plank walls, interior cutaway showing cot with newspapers, table and saint pictures, orthographic architectural style, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 4,500 (含内饰简易桌床) |
| **骨骼与绑定 (Rigging)** | 静态建筑 |
| **贴图槽位 (PBR Maps)** | 1024x1024 贴图图集 (Atlas PBR) |
| **物理碰撞体 (Colliders)** | Composite Box Colliders (墙壁、地面、床体) |
| **挂点与交互节点 (Sockets)** | Bed_Sleep_Spot, Mast_Lean_Wall_Spot, Door_Exit_Point |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/environment/santiagos-shack/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
