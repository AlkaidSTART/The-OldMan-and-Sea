# 3D 资产规格说明书：鲨鱼加工厂起吊木架 (Shark Factory Hoist & Tackle)

> **英文标识 (ID)**: `shark-factory-hoist`  
> **资产层级**: `environment/shark-factory-hoist`  
> **标准规格**: 高 5m, 宽 4m  

---

## 1. 原著设定与生理细节
海湾对岸鲨鱼加工厂的重型木构A字吊架，配有生锈滑轮组与铁吊钩，用于起吊鲨鱼剥皮、取肝、割鳍并剔下肉条腌制，散发浓重气味。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D industrial structure prop of a coastal shark butchering hoist. Heavy A-frame timber crane structure with iron block-and-tackle pulleys, dangling iron meat hooks, wooden gutting tables, blood-stained slipway ramp, 2000 triangles.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
prop sheet of a vintage coastal shark factory A-frame hoist, block and tackle pulleys, iron meat hooks, timber framing, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 2,100 |
| **骨骼与绑定 (Rigging)** | 1 根 (悬挂吊钩摆动) |
| **贴图槽位 (PBR Maps)** | 1024x1024 PBR (重木架、生锈铁索滑轮、污迹滑道) |
| **物理碰撞体 (Colliders)** | Box / Capsule Colliders |
| **挂点与交互节点 (Sockets)** | Shark_Hook_Hang_Point |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/environment/shark-factory-hoist/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
