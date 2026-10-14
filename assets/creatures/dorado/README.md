# 3D 资产规格说明书：鲯鳅 / 鬼头刀 (Dorado / Dolphin Fish)

> **英文标识 (ID)**: `dorado`  
> **资产层级**: `creatures/dorado`  
> **标准规格**: 体长 1.2m, 陡峭高额头  

---

## 1. 原著设定与生理细节
体侧金黄与碧绿相间，死前剧烈变换发光色彩，肉质甜美，胃内常有小飞鱼，是老人的核心体力补给。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D fish model of a vibrant male Mahi-Mahi / Dorado (dolphin fish). Blunt steep forehead, long continuous dorsal fin from head to tail. Iridescent golden-yellow flanks fading into electric green and turquoise spots. Swimming pose, clean quad mesh, mobile and WebGL friendly, 1200 triangles, specular shimmer PBR map.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
fish turnaround sheet of a vibrant Dorado, side view, top view, brilliant green and gold colors, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 1,200 |
| **骨骼与绑定 (Rigging)** | 8 根脊柱骨骼 |
| **贴图槽位 (PBR Maps)** | 512x512 PBR |
| **物理碰撞体 (Colliders)** | Box Collider |
| **挂点与交互节点 (Sockets)** | Bait_Socket |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/creatures/dorado/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
