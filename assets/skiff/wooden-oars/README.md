# 3D 资产规格说明书：双木桨 (Pair of Wooden Oars)

> **英文标识 (ID)**: `wooden-oars`  
> **资产层级**: `skiff/wooden-oars`  
> **标准规格**: 长度 2.7m (每支)  

---

## 1. 原著设定与生理细节
坚韧的长柄重木桨，握把处被老人的手掌磨出油亮光泽，桨叶边缘微有海水撞击碎屑磕痕。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D prop asset of a pair of vintage wooden oars. Long slender shafts with smoothed grip handles, carved flat wooden blades with chipped weathered edges and salt crust. Hand-painted low-poly, 400 triangles per oar, PBR roughness map.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
prop sheet of vintage wooden oars, blade detail, handle grip, top and side view, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 400 x 2 = 800 |
| **骨骼与绑定 (Rigging)** | 无 (挂接于船舷 Rowlock) |
| **贴图槽位 (PBR Maps)** | 512x512 PBR |
| **物理碰撞体 (Colliders)** | Capsule Collider |
| **挂点与交互节点 (Sockets)** | Rowlock_Pivot, Hand_Grip |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/skiff/wooden-oars/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
