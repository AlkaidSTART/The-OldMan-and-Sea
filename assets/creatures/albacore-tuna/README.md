# 3D 资产规格说明书：长鳍金枪鱼 / 鲣鱼 (Albacore / Bonito)

> **英文标识 (ID)**: `albacore-tuna`  
> **资产层级**: `creatures/albacore-tuna`  
> **标准规格**: 体长 0.7m, 10磅重  

---

## 1. 原著设定与生理细节
坚实的纺锤型深海鱼，黑蓝背脊与银白肚皮，老人第一日捕获并切成肉条作为百寻深海诱饵与紧急干粮。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D small game fish model of an Albacore tuna / Bonito. Compact muscular spindle-shaped body, dark navy dorsal back with subtle wavy markings, bright silver belly, small yellow dorsal finlets. 800 triangles, game ready prop for bait or food, PBR textures.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
orthographic sheet of an Albacore bonito tuna, side profile, top view, clean 3D asset guide. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 800 |
| **骨骼与绑定 (Rigging)** | 6 根骨骼 |
| **贴图槽位 (PBR Maps)** | 512x512 PBR |
| **物理碰撞体 (Colliders)** | Capsule Collider |
| **挂点与交互节点 (Sockets)** | Cut_Slice_Point |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/creatures/albacore-tuna/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
