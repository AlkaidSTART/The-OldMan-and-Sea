# 3D 资产规格说明书：船木舵与可拆卸舵柄 (Rudder & Detachable Tiller)

> **英文标识 (ID)**: `rudder-and-tiller`  
> **资产层级**: `skiff/rudder-and-tiller`  
> **标准规格**: 舵板高 0.9m, 舵柄长 0.85m  

---

## 1. 原著设定与生理细节
安装于船艉的简朴木舵板，顶部插接可随时拔出的厚实橡木舵柄，是控制船向与最后关头武器的核心构件。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D interactive prop of a wooden boat rudder assembly with a detachable curved oak tiller handle. Iron hinge pintles with rust, sun-bleached timber, modular components split into rudder blade and separate tiller bar, 600 triangles total, game ready.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
model sheet of wooden boat rudder with detachable curved tiller handle, iron pintles, side and top view. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 650 (舵板 350 + 舵柄 300) |
| **骨骼与绑定 (Rigging)** | 2 根 (铰链旋转) |
| **贴图槽位 (PBR Maps)** | 512x512 PBR |
| **物理碰撞体 (Colliders)** | Box Collider |
| **挂点与交互节点 (Sockets)** | Rudder_Mount, Tiller_Insert_Socket |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/skiff/rudder-and-tiller/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
