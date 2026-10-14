# 3D 资产规格说明书：犁头角鲨 / 铲鼻鲨 (Galano / Brown Scavenger)

> **英文标识 (ID)**: `galano-shark`  
> **资产层级**: `creatures/galano-shark`  
> **标准规格**: 体长 2.5m, 棕褐色铲头食腐群  

---

## 1. 原著设定与生理细节
成双成对出现的食腐恶鲨，头部扁平如铁铲，胸鳍展开如宽铁锹，散发恶臭，专咬水下鱼腹下盘。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D creature model of a brown shovel-nosed scavenger shark (Galano). Wide, flattened shovel-like snout, dull mottled yellowish-brown rough skin texture, broad spade-shaped pectoral fins, murky dull reptilian eyes. Scavenger appearance, robust low-poly game model, clean quad geometry, optimized for WebGL, under 5000 triangles, PBR textures with sand-like roughness.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
creature turnaround sheet of a brown shovel-nosed shark, flat spade head, side and top orthographic views, pure white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 4,500 - 5,500 |
| **骨骼与绑定 (Rigging)** | 18 根骨骼 |
| **贴图槽位 (PBR Maps)** | 1024x1024 PBR (粗砂粒感暗黄表皮) |
| **物理碰撞体 (Colliders)** | Capsule Collider |
| **挂点与交互节点 (Sockets)** | Mouth_Socket |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/creatures/galano-shark/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
