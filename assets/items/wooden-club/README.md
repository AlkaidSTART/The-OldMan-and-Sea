# 3D 资产规格说明书：敲鱼短木棍 (Wooden Fish Club)

> **英文标识 (ID)**: `wooden-club`  
> **资产层级**: `items/wooden-club`  
> **标准规格**: 长 0.75m, 直径 0.08m  

---

## 1. 原著设定与生理细节
削去断桨柄制成的重质短棍，握把刻有防滑槽，头部粗大有反复敲击留下的凹痕，专门击昏金枪鱼与砸击逼近的鲨鱼吻部。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D melee weapon prop of a heavy wooden fish club. Short baton carved from dense oak, notched handle for grip, heavy weighted rounded head with dented impact marks and wood splintering, 300 triangles, Unity Web ready.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
prop model sheet of a heavy wooden fish club, dented weighted head, notched handle, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 320 |
| **骨骼与绑定 (Rigging)** | 静态 |
| **贴图槽位 (PBR Maps)** | 512x512 PBR |
| **物理碰撞体 (Colliders)** | Capsule Collider |
| **挂点与交互节点 (Sockets)** | Handle_Socket, Hit_Volume |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/items/wooden-club/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
