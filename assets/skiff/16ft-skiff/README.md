# 3D 资产规格说明书：16英尺古巴木质小艇 (The 16-foot Skiff)

> **英文标识 (ID)**: `16ft-skiff`  
> **资产层级**: `skiff/16ft-skiff`  
> **标准规格**: 长 4.88m (16ft), 宽 1.4m, 吃水深 0.4m  

---

## 1. 原著设定与生理细节
浸泡数十年的银灰色风化原木，斑驳脱落的淡绿旧漆，3道横木座板，内设木质桨环与缆绳柱，可容纳玩家在甲板自由移动。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D vehicle model of a vintage 1940s Cuban fishing skiff, 16 feet long. Weathered silver-gray wooden plank hull, patches of peeled sea-foam green paint, sun-bleached interior floorboards. Three wooden thwarts (seats), wooden rowlocks on gunwales, bow mooring bitt, stern transom. Open interior hollowed out for player movement, clean topology, LOD ready, 4500 triangles, Unity WebGL asset, realistic rough wood PBR texture.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
model sheet of a 16-foot wooden skiff boat, showing top-down interior layout, side exterior profile, and front/rear cross section. Weathered coastal fishing dory, peeling paint on planks, three bench seats, pure white background, orthographic projection, 3D game asset reference. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 4,200 (LOD0), 1,800 (LOD1) |
| **骨骼与绑定 (Rigging)** | 4 根 (左右桨环旋转 + 舵轴) |
| **贴图槽位 (PBR Maps)** | 1024x1024 PBR (风化木板、脱漆涂层、海盐结晶法线) |
| **物理碰撞体 (Colliders)** | Mesh Collider (Hollow concave interior + Convex hull for water buoyancy) |
| **挂点与交互节点 (Sockets)** | Mast_Socket, Left_Rowlock_Socket, Right_Rowlock_Socket, Stern_Rudder_Socket, Starboard_Marlin_Lash_Socket |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/skiff/16ft-skiff/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
