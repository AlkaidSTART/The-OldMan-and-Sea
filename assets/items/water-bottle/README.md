# 3D 资产规格说明书：深色玻璃淡水瓶 (Brown Glass Water Bottle)

> **英文标识 (ID)**: `water-bottle`  
> **资产层级**: `items/water-bottle`  
> **标准规格**: 高度 0.3m, 容量 1.5L  

---

## 1. 原著设定与生理细节
深色琥珀棕色旧玻璃瓶，软木塞密封，装载老人出海的三天救命淡水（全游戏限饮5口），具有微弱透光水面。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D prop of a vintage 1940s dark amber-brown glass bottle with a tapered neck and pressed cork stopper, filled with half water, slight glass translucency, 400 triangles.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
prop sheet of vintage brown amber glass bottle, cork stopper, liquid level visible, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 420 |
| **骨骼与绑定 (Rigging)** | 静态 |
| **贴图槽位 (PBR Maps)** | 256x256 (玻璃折射/半透明) |
| **物理碰撞体 (Colliders)** | Capsule Collider |
| **挂点与交互节点 (Sockets)** | Mouth_Drink_Socket |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/items/water-bottle/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
