# 3D 资产规格说明书：木桅杆与面粉袋补丁帆 (Mast & Patched Sail)

> **英文标识 (ID)**: `mast-and-patched-sail`  
> **资产层级**: `skiff/mast-and-patched-sail`  
> **标准规格**: 桅杆长 3.8m, 直径 0.1m  

---

## 1. 原著设定与生理细节
单根松木桅杆，帆布由带有印花商业字样的白色面粉袋拼接修补而成，卷起时宛若永不屈服但饱受摧残的战败旗帜。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D game prop of a single pine wood mast with a furled canvas sail wrapped around it. The canvas is patched with faded flour sack cloths showing vintage stamped typography. Tied with thin hemp rope cords. Weathered wood grain, rough fabric folds, low-poly 1200 triangles, PBR material.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
model sheet of a rustic wooden mast, furled canvas patched with flour sack logos, rope ties, side and detail view, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 1,200 |
| **骨骼与绑定 (Rigging)** | 静态 / 展开状态带布料 Cloth 模拟顶点 |
| **贴图槽位 (PBR Maps)** | 1024x1024 PBR (粗麻布、面粉袋文字贴花、圆木) |
| **物理碰撞体 (Colliders)** | Capsule Collider |
| **挂点与交互节点 (Sockets)** | Base_Peg, Top_Stay_Rope |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/skiff/mast-and-patched-sail/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
