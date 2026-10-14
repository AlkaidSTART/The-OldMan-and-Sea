# 《老人与海》Unity Web 3D 资产建模全套 AI 提示词库 (3D Modeling & Asset Prompts)

> 本文档为 Unity Web 游戏《老人与海》全套 3D 资产建模定制。涵盖角色、海洋巨兽、船体、渔具冷兵器、陆地场景道具。
> 提供两套工业级 AI 生成 Prompt：
> 1. **Text-to-3D 引擎 Prompt**（适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie）
> 2. **三视图正交图 Prompt**（适用 Midjourney v6 / SDXL，生成前/侧/后三视图后送入 Image-to-3D 管线）
> 附带 Unity WebGL 面数（Triangles）与贴图预算标准。

---

## 目录
1. [Unity WebGL 3D 资产性能预算总表](#1-unity-webgl-3d-资产性能预算总表)
2. [角色类 3D 资产 (Characters)](#2-角色类-3d-资产-characters)
   - 2.1 [老渔夫圣地亚哥 (Santiago)](#21-老渔夫圣地亚哥-santiago)
   - 2.2 [男孩马诺林 (Manolin)](#22-男孩马诺林-manolin)
   - 2.3 [码头老渔夫佩德里科与游客 (NPCs)](#23-码头老渔夫佩德里科与游客-npcs)
3. [海洋生物类 3D 资产 (Marine Fauna)](#3-海洋生物类-3d-资产-marine-fauna)
   - 3.1 [18英尺巨大大西洋蓝马林鱼 (Giant Marlin)](#31-18英尺巨大大西洋蓝马林鱼-giant-marlin)
   - 3.2 [18英尺马林鱼白骨残骸 (Marlin Skeleton)](#32-18英尺马林鱼白骨残骸-marlin-skeleton)
   - 3.3 [灰鲭鲨 (Mako / Dentuso)](#33-灰鲭鲨-mako--dentuso)
   - 3.4 [犁头铲鼻角鲨 (Shovel-nosed Shark / Galano)](#34-犁头铲鼻角鲨-shovel-nosed-shark--galano)
   - 3.5 [鲯鳅 (Dorado / Dolphin Fish)](#35-鲯鳅-dorado--dolphin-fish)
   - 3.6 [长鳍金枪鱼 (Albacore / Bonito)](#36-长鳍金枪鱼-albacore--bonito)
   - 3.7 [飞鱼 (Flying Fish)](#37-飞鱼-flying-fish)
   - 3.8 [僧帽水母 (Portuguese Man-of-War / Agua Mala)](#38-僧帽水母-portuguese-man-of-war--agua-mala)
   - 3.9 [海龟与海鸟 (Turtle, Warbler, Frigate Bird)](#39-海龟与海鸟-turtle-warbler-frigate-bird)
4. [载具与甲板构件 3D 资产 (The Skiff & Rigging)](#4-载具与甲板构件-3d-资产-the-skiff--rigging)
   - 4.1 [16英尺古巴木质小艇 (The 16-foot Skiff)](#41-16英尺古巴木质小艇-the-16-foot-skiff)
   - 4.2 [木桅杆与面粉袋补丁帆 (Mast & Patched Sail)](#42-木桅杆与面粉袋补丁帆-mast--patched-sail)
   - 4.3 [双木桨 (Pair of Wooden Oars)](#43-双木桨-pair-of-wooden-oars)
   - 4.4 [船木舵与可拆卸舵柄 (Rudder & Detachable Tiller)](#44-船木舵与可拆卸舵柄-rudder--detachable-tiller)
   - 4.5 [战损尖刺残木舵柄 (Splintered Tiller Weapon)](#45-战损尖刺残木舵柄-splintered-tiller-weapon)
5. [渔具与冷兵器道具 3D 资产 (Gear & Weapons)](#5-渔具与冷兵器道具-3d-资产-gear--weapons)
   - 5.1 [生锈铁飞叉与长索 (Iron Harpoon & Line)](#51-生锈铁飞叉与长索-iron-harpoon--line)
   - 5.2 [绑刀船桨临时长矛 (Improvised Spear: Knife on Oar)](#52-绑刀船桨临时长矛-improvised-spear-knife-on-oar)
   - 5.3 [敲鱼短木棍 (Wooden Club)](#53-敲鱼短木棍-wooden-club)
   - 5.4 [柳条线篮、水壶与饵木箱 (Baskets, Bottle, Bait Box)](#54-柳条线篮水壶与饵木箱-baskets-bottle-bait-box)
6. [陆地建筑与环境小品 3D 资产 (Environment & Buildings)](#6-陆地建筑与环境小品-3d-资产-environment--buildings)
   - 6.1 [老人的木棚陋室 (Santiago's Shack)](#61-老人的木棚陋室-santiagos-shack)
   - 6.2 [露台饭店码头栈桥与鲨鱼起吊架 (Docks & Shark Hoist)](#62-露台饭店码头栈桥与鲨鱼起吊架-docks--shark-hoist)

---

## 1. Unity WebGL 3D 资产性能预算总表

为了保证在浏览器端稳定 60 FPS 运行并控制打包体积在 30MB 内，所有 3D 资产严格遵守以下三角面与贴图标准：

| 资产类别 | 最大三角面数 (Max Tris) | LOD层级 | 贴图分辨率 (PBR) | 骨骼数量 (Bones) |
| :--- | :--- | :--- | :--- | :--- |
| **主角：圣地亚哥** | 12,000 - 15,000 | LOD0 / LOD1 (6k) | 1024x1024 (Albedo/Normal/Roughness) | 52 根 (标准 Humanoid) |
| **Boss：18英尺大马林鱼** | 10,000 - 14,000 | LOD0 / LOD1 (5k) | 1024x1024 (带 Emission 紫条纹) | 28 根 (脊椎链+鳍) |
| **马林鱼白骨残骸** | 6,000 - 8,000 | 单一Mesh | 1024x1024 | 12 根 (随浪轻摆) |
| **鲨鱼类 (灰鲭/铲头)** | 5,000 - 8,000 | LOD0 / LOD1 (3k) | 1024x1024 | 20 根 |
| **中小型生态鱼类** | 600 - 1,500 | 无 | 512x512 | 6 - 10 根 |
| **小木艇 (含内饰)** | 4,000 - 6,000 | LOD0 / LOD1 (2k) | 1024x1024 (旧木漫反射) | 4 根 (舵/桨环) |
| **武器手持道具** | 300 - 800 | 无 | 512x512 | 无 (挂点绑骨) |
| **陆地建筑/码头** | 3,000 - 5,000 | LOD0 / LOD1 (1.5k) | 1024x1024 贴图图集 (Atlas) | 静态 |

---

## 2. 角色类 3D 资产 (Characters)

### 2.1 老渔夫圣地亚哥 (Santiago)

* **原著特征**：极度消瘦苍老，颈后深刻刀刻皱纹，脸颊阳光反射引发的褐色良性皮肤癌斑，湛蓝坚毅不败的眼睛，手上勒痕老茧与抽筋手爪，粗麻布面粉袋补丁衬衫，卷裤腿，赤脚。

#### A. Text-to-3D 提示词 (Meshy / Tripo3D / Rodin)
```text
3D game character model of an elderly Cuban fisherman, Santiago from The Old Man and the Sea. Gaunt, thin, sinewy athletic build, deep weathered facial wrinkles, dark bronze sunburnt skin with faint brown age spots on cheeks. Striking deep ocean-blue eyes. Wearing a faded, unbuttoned ragged linen shirt with patched flour sack cloths, frayed cuffs, rolled-up brown canvas trousers stopped at the knees, barefoot with weathered feet. Old scars and calluses on hands. A-pose, neutral expression, clean quad topology, low-poly game ready asset, optimized for Unity WebGL, PBR textures, stylized realistic art style.
```

#### B. 三视图正交图生成 (Midjourney v6 - Image-to-3D 预处理)
```text
character sheet, 3d model turnaround sheet of Santiago, an old Cuban fisherman, showing front view, side view, and back view. Standing in neutral A-pose on neutral gray background. Thin gaunt sinewy body, deeply tanned weathered brown skin, deep neck wrinkles, piercing blue eyes. Wearing a faded off-white patched shirt, tattered canvas pants rolled to mid-calf, barefoot. Clean orthographic projection, no shadows, consistent lighting, symmetrical, character design for 3D modeling, artstation trending, 8k.
--ar 16:9 --v 6.0
```

---

### 2.2 男孩马诺林 (Manolin)

* **原著特征**：14-16 岁古巴少年渔童，黑发，古铜色皮肤，健朗忠诚的眼神，身穿简朴渔民背心与粗布短裤。

#### A. Text-to-3D 提示词
```text
3D low-poly character model of a young 15-year-old Cuban fisher boy, Manolin. Youthful sun-kissed tan skin, friendly determined dark eyes, messy black short hair. Wearing a simple beige sleeveless cotton work shirt, faded navy blue shorts, barefoot. Athletic lean build, clean topology, A-pose, game ready character asset for Unity WebGL, hand-painted PBR texture, neutral lighting.
```

#### B. 三视图正交图生成
```text
character turnaround sheet, front view, side view, back view of Manolin, a young Cuban fisher boy in A-pose. White background, orthographic camera, clean lines, wearing a loose tan vest and dark rolled shorts, barefoot. 3D model reference sheet, symmetrical, stylized realism, Unreal Engine 5 aesthetic.
--ar 16:9 --v 6.0
```

---

### 2.3 码头老渔夫佩德里科与游客 (NPCs)

* **老渔夫佩德里科 (Pedrico)**：
  ```text
  Low-poly 3D NPC model of an older Cuban dock fisherman, wearing a battered straw fedora hat, faded blue dungarees, rubber fishing boots, holding a coiled measuring rope, friendly weathered face, Unity WebGL asset, under 3000 triangles.
  ```
* **露台女游客 (The Tourist Woman)**：
  ```text
  Low-poly 1950s stylish female tourist NPC, wearing a vintage light yellow summer dress, cat-eye sunglasses, sunhat, elegant standing pose, low poly count for background web crowd, clean PBR texture.
  ```

---

## 3. 海洋生物类 3D 资产 (Marine Fauna)

### 3.1 18英尺巨大大西洋蓝马林鱼 (Giant Marlin)

* **原著特征**：体长 18 英尺（5.5米），极具流线型；剑吻长如棒球棍并尖锐如花剑；背鳍呈暗紫色镰刀形；体侧发光浅紫色横纹；腹部银白光滑；新月形巨尾。

#### A. Text-to-3D 提示词 (Meshy / Tripo3D / Rodin)
```text
3D game boss creature model of a colossal 18-foot Atlantic Blue Marlin. Hydrodynamic muscular billfish body, long tapered rapier-like spear beak (bill), huge dark-purple sickle-shaped dorsal fin along the spine, large crescent tail fin, broad pectoral fins like bird wings. Dark cobalt-blue dorsal back, glowing vivid lavender-purple vertical stripes across the flanks, burnished reflective silver belly. Symmetrical bind pose, clean topology for swimming animations, PBR roughness and metallic maps, emission map for purple stripes, Unity WebGL optimized.
```

#### B. 三视图正交图生成
```text
creature turnaround sheet of an 18-foot giant blue marlin, showing side view, top view, front view. Orthographic projection, pure white background. Sleek torpedo body, long pointed bill, large crescent tail, glowing lavender vertical stripes on deep blue sides, silver belly. Model sheet for 3D sculptors, accurate marine biology proportions, 8k.
--ar 16:9 --v 6.0
```

---

### 3.2 18英尺马林鱼白骨残骸 (Marlin Skeleton)

* **原著特征**：肉被鲨鱼啃食殆尽后仅剩的纯白骨架。修长的脊椎骨、如肋排般的骨刺、带吻剑的完整鱼头颅骨、以及末端硕大优雅的新月尾鳍。

#### A. Text-to-3D 提示词
```text
3D game prop model of a colossal 18-foot marlin skeleton. Pure bleached white spine backbone, rib bones curving down, skull with the long pointed bill sword intact, and the large intact dark crescent tail at the far end. Flesh completely stripped, hollow eye sockets, washed-out driftwood texture with salt crystals, game-ready environment prop, Unity WebGL asset, modular mesh, under 6000 triangles.
```

#### B. 正交图生成
```text
isolated model sheet of an 18-foot giant marlin fish skeleton, side profile and perspective view. Bleached white backbone and ribs, long pointed bill skull, crescent tail fin remaining at the back. Studio lighting, neutral grey background, clean bone geometry reference for 3D modeling.
--ar 16:9 --v 6.0
```

---

### 3.3 灰鲭鲨 (Mako / Dentuso)

* **原著特征**：深青蓝色背脊、银色腹部，如鱼雷般的高速流线型，口内有八排如人爪弯曲的利齿。

#### A. Text-to-3D 提示词
```text
3D creature model of an apex predator Mako shark (Dentuso). Streamlined hydrodynamic torpedo body, vibrant metallic deep-blue back, crisp white underbelly. Jaws slightly open displaying rows of curved inward claw-like razor-sharp white teeth. Dead black predatory eyes, tall sharp dorsal fin, symmetrical swimming T-pose, clean edge loop topology for spine flexing, low-poly Unity WebGL game asset, PBR material.
```

#### B. 三视图正交图生成
```text
creature model sheet of a Mako shark, showing side profile, top-down view, and front-facing open mouth view. Deep blue metallic back, pure white belly, claw teeth visible, orthographic camera, neutral background, 3D model reference sheet, photorealistic texture guide.
--ar 16:9 --v 6.0
```

---

### 3.4 犁头铲鼻角鲨 (Shovel-nosed Shark / Galano)

* **原著特征**：棕黄色、扁平如铲的头部、铁锹般的宽阔胸鳍、食腐恶臭的下层掠食者。

#### A. Text-to-3D 提示词
```text
3D creature model of a brown shovel-nosed scavenger shark (Galano). Wide, flattened shovel-like snout, dull mottled yellowish-brown rough skin texture, broad spade-shaped pectoral fins, murky dull reptilian eyes. Scavenger appearance, robust low-poly game model, clean quad geometry, optimized for WebGL, under 5000 triangles, PBR textures with sand-like roughness.
```

---

### 3.5 鲯鳅 (Dorado / Dolphin Fish)

* **原著特征**：头部高耸扁平，体色为金黄、翠绿交织，死前闪烁蓝光，身形优美。

#### A. Text-to-3D 提示词
```text
3D fish model of a vibrant male Mahi-Mahi / Dorado (dolphin fish). Blunt steep forehead, long continuous dorsal fin from head to tail. Iridescent golden-yellow flanks fading into electric green and turquoise spots. Swimming pose, clean quad mesh, mobile and WebGL friendly, 1200 triangles, specular shimmer PBR map.
```

---

### 3.6 长鳍金枪鱼 (Albacore / Bonito)

* **原著特征**：10 磅重，纺锤体型，深蓝黑背部，银白腹部，坚实肉质。

#### A. Text-to-3D 提示词
```text
3D small game fish model of an Albacore tuna / Bonito. Compact muscular spindle-shaped body, dark navy dorsal back with subtle wavy markings, bright silver belly, small yellow dorsal finlets. 800 triangles, game ready prop for bait or food, PBR textures.
```

---

### 3.7 飞鱼 (Flying Fish)

* **原著特征**：体态轻盈修长，胸鳍特化为如蝉翼般的透明大翅膀。

#### A. Text-to-3D 提示词
```text
3D miniature fish model of a tropical flying fish. Slim silver-blue body, dramatically enlarged wing-like translucent pectoral fins with delicate blue veins. Gliding pose, alpha-tested transparent wing texture, under 600 triangles, lightweight Unity Web asset.
```

---

### 3.8 僧帽水母 (Portuguese Man-of-War / Agua Mala)

* **原著特征**：漂浮在水面的半透明紫红色虹彩气囊，下方拖着数十根深蓝色剧毒触须。

#### A. Text-to-3D 提示词
```text
3D marine prop model of a Portuguese Man-of-War jellyfish. Iridescent translucent crest balloon floating on water surface, shifting pink, violet, and electric blue hues. Cluster of delicate tangled dark purple stinging tentacles hanging below. Subsurface scattering transparent glass shader, low-poly optimized, under 1500 triangles.
```

---

### 3.9 海龟与海鸟 (Turtle, Warbler, Frigate Bird)

* **大蠵龟 (Loggerhead Turtle)**：
  ```text
  3D sea turtle model, rough brownish-yellow textured carapace shell, paddle flippers with blunt claws, ancient wrinkled neck and head, slow swimming pose, 2500 triangles, PBR textures.
  ```
* **疲倦的黄林莺 (Tired Warbler)**：
  ```text
  3D tiny songbird model, yellow and olive-green warbler, perched pose with claws gripping a cord, ruffled exhausted feathers, low poly, 500 triangles.
  ```
* **军舰鸟 (Frigate Bird)**：
  ```text
  3D soaring bird model, large dark black seabird, long hooked beak, distinct deeply forked scissor tail, wings outstretched gliding pose, 1000 triangles.
  ```

---

## 4. 载具与甲板构件 3D 资产 (The Skiff & Rigging)

### 4.1 16英尺古巴木质小艇 (The 16-foot Skiff)

* **原著特征**：长 16 英尺的小木船，风化银灰色原木、剥落的浅绿漆、船内横座板（Thwarts）、木质划桨环、船艏系缆木桩。

#### A. Text-to-3D 提示词
```text
3D vehicle model of a vintage 1940s Cuban fishing skiff, 16 feet long. Weathered silver-gray wooden plank hull, patches of peeled sea-foam green paint, sun-bleached interior floorboards. Three wooden thwarts (seats), wooden rowlocks on gunwales, bow mooring bitt, stern transom. Open interior hollowed out for player movement, clean topology, LOD ready, 4500 triangles, Unity WebGL asset, realistic rough wood PBR texture.
```

#### B. 三视图正交图生成
```text
model sheet of a 16-foot wooden skiff boat, showing top-down interior layout, side exterior profile, and front/rear cross section. Weathered coastal fishing dory, peeling paint on planks, three bench seats, pure white background, orthographic projection, 3D game asset reference.
--ar 16:9 --v 6.0
```

---

### 4.2 木桅杆与面粉袋补丁帆 (Mast & Patched Sail)

* **原著特征**：单根粗圆木桅杆，帆布用印有商业字样的白色面粉麻袋拼凑修补而成，卷起时如战败之旗。

#### A. Text-to-3D 提示词
```text
3D game prop of a single pine wood mast with a furled canvas sail wrapped around it. The canvas is patched with faded flour sack cloths showing vintage stamped typography. Tied with thin hemp rope cords. Weathered wood grain, rough fabric folds, low-poly 1200 triangles, PBR material.
```

---

### 4.3 双木桨 (Pair of Wooden Oars)

* **原著特征**：长柄重木桨，把手处磨出老茧光泽，桨叶末端受海水侵蚀有轻微磕碰缺口。

#### A. Text-to-3D 提示词
```text
3D prop asset of a pair of vintage wooden oars. Long slender shafts with smoothed grip handles, carved flat wooden blades with chipped weathered edges and salt crust. Hand-painted low-poly, 400 triangles per oar, PBR roughness map.
```

---

### 4.4 船木舵与可拆卸舵柄 (Rudder & Detachable Tiller)

* **原著特征**：船艉插接式木舵板，顶部插有可随时单手拔出的厚实硬木舵柄（Tiller）。

#### A. Text-to-3D 提示词
```text
3D interactive prop of a wooden boat rudder assembly with a detachable curved oak tiller handle. Iron hinge pintles with rust, sun-bleached timber, modular components split into rudder blade and separate tiller bar, 600 triangles total, game ready.
```

---

### 4.5 战损尖刺残木舵柄 (Splintered Tiller Weapon)

* **原著特征**：在怒砸鲨鱼后中断折裂的舵柄，前端形成尖锐参差的木刺断口，作为最终自卫武器。

#### A. Text-to-3D 提示词
```text
3D improvised weapon prop of a snapped broken wooden tiller handle. The front end is shattered into a jagged, sharp spear-like wooden splinter point stained with dried dark blood and seawater. Handgrip wrapped in dirty twine, 350 triangles, WebGL weapon model.
```

---

## 5. 渔具与冷兵器道具 3D 资产 (Gear & Weapons)

### 5.1 生锈铁飞叉与长索 (Iron Harpoon & Line)

* **原著特征**：粗糙锻造的单倒钩铁飞叉头，牢固嵌在长木杆上，尾部系有一圈圈整齐盘绕的粗麻绳。

#### A. Text-to-3D 提示词
```text
3D weapon prop of a vintage hand-forged iron whaling harpoon. Dark pitted iron spearhead with a sharp single barbed point, mounted onto a sturdy ash-wood pole with copper wrapping. A coil of thick tarred hemp rope attached to the base. 750 triangles, PBR metallic and rust textures.
```

---

### 5.2 绑刀船桨临时长矛 (Improvised Spear: Knife on Oar)

* **原著特征**：水手折刀刀刃展开，用粗糙湿皮带和钓线紧紧缠绕绑在一柄长木桨顶端。

#### A. Text-to-3D 提示词
```text
3D makeshift weapon prop, a sailor's folding pocketknife with exposed blade lashed firmly to the blade tip of a long wooden oar using crisscrossed raw leather thongs and green fishing cord. Improvised ocean spear, rugged survivalist design, 600 triangles, clean PBR textures.
```

---

### 5.3 敲鱼短木棍 (Wooden Club)

* **原著特征**：长约两英尺半，由老废桨柄砍削而成的硬木短棒，专门敲击金枪鱼和击打靠近的鲨鱼鼻梁。

#### A. Text-to-3D 提示词
```text
3D melee weapon prop of a heavy wooden fish club. Short baton carved from dense oak, notched handle for grip, heavy weighted rounded head with dented impact marks and wood splintering, 300 triangles, Unity Web ready.
```

---

### 5.4 柳条线篮、水壶与饵木箱 (Baskets, Bottle, Bait Box)

* **编织钓线柳条篮 (Wicker Line Basket)**：
  ```text
  3D prop of a round woven wicker basket containing neatly coiled thick green braided fishing cord, 500 triangles, clean PBR normal map.
  ```
* **棕色玻璃淡水瓶 (Brown Glass Water Bottle)**：
  ```text
  3D prop of a vintage 1940s dark amber-brown glass bottle with a tapered neck and pressed cork stopper, filled with half water, slight glass translucency, 400 triangles.
  ```
* **粗盐诱饵木箱 (Salt Bait Box)**：
  ```text
  3D prop of a small hinged wooden crate covered in coarse white rock salt crystals, with two salted sardines resting inside, 500 triangles.
  ```

---

## 6. 陆地建筑与环境小品 3D 资产 (Environment & Buildings)

### 6.1 老人的木棚陋室 (Santiago's Shack)

* **原著特征**：古巴海边简陋木板棚屋，屋顶覆盖皇家棕榈叶（Guano），内部一张弹簧床垫上铺满旧报纸，墙上挂着彩色宗教画像，木桌与洗脸盆。

#### A. Text-to-3D 提示词
```text
3D low-poly building interior and exterior of Santiago's humble coastal shack. Walls made of tough budshields of royal palm (guano) and gray weathered timber planks, dirt floor, thatched palm leaf roof. Interior includes a rustic wooden cot with springs covered in old yellowed newspapers, a wobbly small table, a wash basin, and framed faded prints of Sacred Heart of Jesus on the wall. Modular layout, optimized for Unity WebGL, under 5000 triangles total, rustic colonial Cuban palette.
```

---

### 6.2 露台饭店码头栈桥与鲨鱼起吊架 (Docks & Shark Hoist)

* **露台饭店木质栈桥码头 (Terrace Wooden Pier)**：
  ```text
  3D environment prop set of a weathered wooden harbor dock pier. Heavy barnacle-encrusted timber pilings, wooden planks with gaps, iron mooring cleats, wooden ladder descending into water, 2500 triangles.
  ```
* **鲨鱼加工厂起吊木架 (Shark Factory Hoist & Tackle)**：
  ```text
  3D industrial structure prop of a coastal shark butchering hoist. Heavy A-frame timber crane structure with iron block-and-tackle pulleys, dangling iron meat hooks, wooden gutting tables, blood-stained slipway ramp, 2000 triangles.
  ```
