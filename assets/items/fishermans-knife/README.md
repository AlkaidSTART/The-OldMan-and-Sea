# 3D 资产规格说明书：老水手折刀 (Fisherman's Pocket Knife)

> **英文标识 (ID)**: `fishermans-knife`  
> **资产层级**: `items/fishermans-knife`  
> **标准规格**: 刀刃长 0.12m, 展开全长 0.26m  

---

## 1. 原著设定与生理细节
老人唯一随身携带的切割短刀，用于削切金枪鱼片生食、割断钓线，后被绑于木桨上并最终折断于鲨鱼骨骼中。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D weapon prop of a vintage sailor's folding pocketknife. Worn carbon steel blade with salt patina, wooden handle with brass bolsters and pins, lanyard hole, open position, 300 triangles, clean PBR textures.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
prop turnaround sheet of sailor folding knife, open and closed state, steel blade, brass wood grip, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 320 |
| **骨骼与绑定 (Rigging)** | 1 根 (刀刃折叠旋转轴) |
| **贴图槽位 (PBR Maps)** | 256x256 PBR |
| **物理碰撞体 (Colliders)** | Box Collider |
| **挂点与交互节点 (Sockets)** | Hand_Grip, Blade_Cutting_Edge |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/items/fishermans-knife/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
