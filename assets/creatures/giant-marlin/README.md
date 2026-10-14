# 3D 资产规格说明书：18英尺巨大大西洋蓝马林鱼 (The Giant Marlin)

> **英文标识 (ID)**: `giant-marlin`  
> **资产层级**: `creatures/giant-marlin`  
> **标准规格**: 体长 18英尺 (约5.5m), 体重超1000磅  

---

## 1. 原著设定与生理细节
体长比老人的16英尺小船还长2英尺；吻剑长如棒球棍、尖若细花剑；暗紫镰刀形高耸背鳍；体侧布满发光淡紫色横斑条纹；腹部银亮如磨光硬币；巨大新月形尾鳍。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D game boss creature model of a colossal 18-foot Atlantic Blue Marlin. Hydrodynamic muscular billfish body, long tapered rapier-like spear beak (bill), huge dark-purple sickle-shaped dorsal fin along the spine, large crescent tail fin, broad pectoral fins like bird wings. Dark cobalt-blue dorsal back, glowing vivid lavender-purple vertical stripes across the flanks, burnished reflective silver belly. Symmetrical bind pose, clean topology for swimming animations, PBR roughness and metallic maps, emission map for purple stripes, Unity WebGL optimized.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
creature turnaround sheet of an 18-foot giant blue marlin, showing side view, top view, front view. Orthographic projection, pure white background. Sleek torpedo body, long pointed bill, large crescent tail, glowing lavender vertical stripes on deep blue sides, silver belly. Model sheet for 3D sculptors, accurate marine biology proportions, 8k. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 10,000 - 14,000 (LOD0), 5,000 (LOD1) |
| **骨骼与绑定 (Rigging)** | 28 根骨骼 (主脊椎链16节 + 胸鳍4 + 尾鳍6 + 颌骨2) |
| **贴图槽位 (PBR Maps)** | 1024x1024 PBR (含 Emission 自发光紫条纹通道) |
| **物理碰撞体 (Colliders)** | Compound Box/Capsule Colliders (头部穿刺区、中躯受力区、尾鳍拍打区) |
| **挂点与交互节点 (Sockets)** | Mouth_Hook_Point (钓钩咬合受力点), Heart_Hit_Point (鱼叉处决点), Bill_Grip_Point (吻部拉绳点) |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/creatures/giant-marlin/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
