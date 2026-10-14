# 3D 资产规格说明书：露台饭店码头栈桥 (The Terrace Dock & Boardwalk)

> **英文标识 (ID)**: `terrace-pier-dock`  
> **资产层级**: `environment/terrace-pier-dock`  
> **标准规格**: 长 18m, 宽 3m  

---

## 1. 原著设定与生理细节
渔船停靠卸货的木质栈桥码头，长年受潮汐冲刷长满藤壶的粗圆木桩，防滑木板条，生锈系缆铁栓，连接着热闹的露台饭店。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D environment prop set of a weathered wooden harbor dock pier. Heavy barnacle-encrusted timber pilings, wooden planks with gaps, iron mooring cleats, wooden ladder descending into water, 2500 triangles.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
architectural model sheet of a vintage Caribbean wooden harbor pier dock, barnacle pilings, mooring cleats, orthographic perspective, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 2,400 |
| **骨骼与绑定 (Rigging)** | 静态 |
| **贴图槽位 (PBR Maps)** | 1024x1024 PBR |
| **物理碰撞体 (Colliders)** | Box Colliders (桥面、斜坡) |
| **挂点与交互节点 (Sockets)** | Skiff_Mooring_Cleat_1, Skiff_Mooring_Cleat_2 |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/environment/terrace-pier-dock/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
