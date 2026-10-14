# 3D 资产规格说明书：老渔夫圣地亚哥 (Santiago)

> **英文标识 (ID)**: `santiago`  
> **资产层级**: `characters/santiago`  
> **标准规格**: 高度 1.78m, 体重 62kg (消瘦健韧)  

---

## 1. 原著设定与生理细节
极度消瘦憔悴，颈后有深邃皱纹；脸颊有热带烈日照射引起的良性皮肤癌褐色斑；双眼与大海同色，欢快且永不言败；双手布满拉扯重鱼留下的老茧伤痕；左手易抽筋；身穿面粉袋粗布补丁旧衬衫、磨损卷边长裤，赤足。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D game character model of an elderly Cuban fisherman, Santiago from The Old Man and the Sea. Gaunt, thin, sinewy athletic build, deep weathered facial wrinkles, dark bronze sunburnt skin with faint brown age spots on cheeks. Striking deep ocean-blue eyes. Wearing a faded, unbuttoned ragged linen shirt with patched flour sack cloths, frayed cuffs, rolled-up brown canvas trousers stopped at the knees, barefoot with weathered feet. Old scars and calluses on hands. A-pose, neutral expression, clean quad topology, low-poly game ready asset, optimized for Unity WebGL, PBR textures, stylized realistic art style.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
character sheet, 3d model turnaround sheet of Santiago, an old Cuban fisherman, showing front view, side view, and back view. Standing in neutral A-pose on neutral gray background. Thin gaunt sinewy body, deeply tanned weathered brown skin, deep neck wrinkles, piercing blue eyes. Wearing a faded off-white patched shirt, tattered canvas pants rolled to mid-calf, barefoot. Clean orthographic projection, no shadows, consistent lighting, symmetrical, character design for 3D modeling, artstation trending, 8k. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 12,000 - 15,000 (LOD0), 6,000 (LOD1) |
| **骨骼与绑定 (Rigging)** | 52 根标准 Unity Humanoid 骨骼 |
| **贴图槽位 (PBR Maps)** | 1024x1024 PBR (Albedo, Normal, Metallic/Roughness, AO, Subsurface Mask) |
| **物理碰撞体 (Colliders)** | Capsule Collider (Radius 0.35m, Height 1.8m) |
| **挂点与交互节点 (Sockets)** | RightHand_Socket (武器/线把手), LeftHand_Socket (擦伤/痉挛状态), Back_Rope_Mount (后背承重钓索点) |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/characters/santiago/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
