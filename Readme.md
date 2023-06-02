# Equanimous Prototype Lab
Build
Test
Iterate
Repeat

testing all my crazy ideas until they can be proven genius

KEEP GOING!!!

## 1. Asset Naming Conventions
Naming conventions should be treated as law. A project that conforms to a naming convention is able to have its assets managed, searched, parsed, and maintained with incredible ease.

Most things are prefixed with the prefix generally being an acronym of the asset type followed by an underscore.

**Assets use [PascalCase](#cases)**

<a name="base-asset-name"></a>
<a name="1.1"></a>
### 1.1 Base Asset Name - `Prefix_BaseAssetName_Variant_Suffix`
All assets should have a _Base Asset Name_. A Base Asset Name represents a logical grouping of related assets. Any asset that is part of this logical group 
should follow the the standard of  `Prefix_BaseAssetName_Variant_Suffix`.

Keeping the pattern `Prefix_BaseAssetName_Variant_Suffix` in mind and using common sense is generally enough to warrant good asset names. Here are some detailed rules regarding each element.

`Prefix` and `Suffix` are to be determined by the asset type through the following [Asset Name Modifier](#asset-name-modifiers) tables.

`BaseAssetName` should be determined by short and easily recognizable name related to the context of this group of assets. For example, if you had a character named Bob, all of Bob's assets would have the `BaseAssetName` of `Bob`.

For unique and specific variations of assets, `Variant` is either a short and easily recognizable name that represents logical grouping of assets that are a subset of an asset's base name. For example, if Bob had multiple skins these skins should still use `Bob` as the `BaseAssetName` but include a recognizable `Variant`. An 'Evil' skin would be referred to as `Bob_Evil` and a 'Retro' skin would be referred to as `Bob_Retro`.

For unique but generic variations of assets, `Variant` is a two digit number starting at `01`. For example, if you have an environment artist generating nondescript rocks, they would be named `Rock_01`, `Rock_02`, `Rock_03`, etc. Except for rare exceptions, you should never require a three digit variant number. If you have more than 100 assets, you should consider organizing them with different base names or using multiple variant names.

Depending on how your asset variants are made, you can chain together variant names. For example, if you are creating flooring assets for an Arch Viz project you should use the base name `Flooring` with chained variants such as `Flooring_Marble_01`, `Flooring_Maple_01`, `Flooring_Tile_Squares_01`.

<a name="1.1-examples"></a>

#### Examples

##### Character

| Asset Type               | Asset Name   |
| ------------------------ | ------------ |
| Skeletal Mesh            | SK_Bob       |
| Material                 | M_Bob        |
| Texture (Diffuse/Albedo) | T_Bob_D      |
| Texture (Normal)         | T_Bob_N      |
| Texture (Evil Diffuse)   | T_Bob_Evil_D |

##### Prop

| Asset Type               | Asset Name   |
| ------------------------ | ------------ |
| Static Mesh (01)         | SM_Rock_01   |
| Static Mesh (02)         | SM_Rock_02   |
| Static Mesh (03)         | SM_Rock_03   |
| Material                 | M_Rock       |
| Material Instance (Snow) | MI_Rock_Snow |

<a name="asset-name-modifiers"></a>

### 1.2 Asset Name Modifiers

When naming an asset use these tables to determine the prefix and suffix to use with an asset's [Base Asset Name](#base-asset-name).

#### Sections

> 1.2.1 [Most Common](#anc-common)

> 1.2.2 [Animations](#anc-animations)

> 1.2.3 [Artificial Intelligence](#anc-ai)

> 1.2.1 [Prefabs](#anc-prefab)

> 1.2.5 [Materials](#anc-materials)

> 1.2.6 [Textures](#anc-textures)

> 1.2.7 [Signed Distance Fields](#anc-sdf)

> 1.2.8 [Shaders](#anc-shaders)

> 1.2.9 [Shader Graphs](#anc-shader-graphs)

> 1.2.10 [Miscellaneous](#anc-misc)

> 1.2.11 [Physics](#anc-physics)

> 1.2.12 [Audio](#anc-audio)

> 1.2.13 [User Interface](#anc-ui)

> 1.2.14 [Effects](#anc-effects)


<a name="anc-common"></a>

#### Most Common

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Level / Scene           |  *          |            | [Should be in a folder called Levels.](#levels) e.g. `Levels/A4_C17_Parking_Garage.unity` |
| Level (Persistent)      |            | _P         |                                  |
| Level (Audio)           |            | _Audio     |                                  |
| Level (Lighting)        |            | _Lighting  |                                  |
| Level (Geometry)        |            | _Geo       |                                  |
| Level (Gameplay)        |            | _Gameplay  |                                  |
| Prefab                  |        |            |                                  |
| Material                | M_         |            |                                  |
| Static Mesh             | SM_       |            |                                  |
| Skeletal Mesh           | SK_       |            |                                  |
| Texture                 | T_         | _?         | See [Textures](#anc-textures)    |
| Signed Distance Field   | SDF_       |            |                                  |
| Shaders                 | SH_       |            |                                  |
| Shader Graphs           | SHG_      |            |                                  |
| Particle System         | PS_       |            |                                  |
| Vissual Effects         | VFX_       |            |                                  |


<a name="anc-models"></a>

#### 1.2.1a 3D Models (FBX Files)

PascalCase

| Asset Type    | Prefix | Suffix | Notes |
| ------------- | ------ | ------ | ----- |
| Characters    | CH_    |        |       |
| Furniture     | FU_    |        |       |
| Icons         | IC_    |        |       |
| Weapons       | WP_    |        |       |
| Static Mesh   | SM_    |        |       |
| Skeletal Mesh | SK_    |        |       |
| Skeleton      | SKEL_  |        |       |
| Rig           | RIG_   |        |       |

#### 1.2.1b 3d Models (3ds Max)

All meshes in 3ds Max are lowercase to differentiate them from their FBX export.

| Asset Type    | Prefix | Suffix      | Notes                                   |
| ------------- | ------ | ----------- | --------------------------------------- |
| Mesh          |        | _mesh_lod0* | Only use LOD suffix if model uses LOD's |
| Mesh Collider |        | _collider   |                                         |


<a name="anc-animations"></a>

#### 1.2.2 Animations

| Asset Type           | Prefix | Suffix | Notes |
| -------------------- | ------ | ------ | ----- |
| Animation Clip       | A_     |        |       |
| Animation Controller | AC_    |        |       |
| Avatar Mask          | AM_    |        |       |
| Morph Target         | MT_    |        |       |


<a name="anc-ai"></a>

#### 1.2.3 Artificial Intelligence

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| AI Controller           | AIC_     |            |                                  |
| Behavior Tree           | BT_      |            |                                  |
| Blackboard              | BB_       |            |                                  |
| Decorator               | BTDecorator_ |          |                                  |
| Service                 | BTService_ |            |                                  |
| Task                    | BTTask_  |            |                                  |
| Environment Query       | EQS_     |            |                                  |
| EnvQueryContext         | EQS_     | Context    |                                  |


<a name="anc-prefab"></a>

#### 1.2.4 Prefabs

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Prefab         |        |            |                                  |
| Prefab Instance         | I       |            |                                  |
| Scriptable Object       |     |        | Assigned "Blueprint" label in Editor |


<a name="anc-materials"></a>

#### 1.2.5 Materials
| Asset Type        | Prefix | Suffix | Notes |
| ----------------- | ------ | ------ | ----- |
| Material          | M_     |        |       |
| Material Instance | MI_    |        |       |
| Physical Material | PM_    |        |       |


<a name="anc-textures"></a>

#### 1.2.6 Textures
| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Texture                 | T_         |            |                                  |
| Texture (Diffuse/Albedo/Base Color)| T_ | _D      |                                  |
| Texture (Normal)        | T_         | _Normal         |                                  |
| Texture (Roughness)     | T_         | _Roughness         |                                  |
| Texture (Alpha/Opacity) | T_         | _Alpha         |                                  |
| Texture (Ambient Occlusion) | T_     | _AO      |                                  |
| Texture (Bump)          | T_         | _Bump         |                                  |
| Texture (Emissive)      | T_         | _Emissive         |                                  |
| Texture (Mask)          | T_         | _Mask         |                                  |
| Texture (Specular)      | T_         | _Specular         |                                  |
| Texture (Packed)        | T_         | _*         | See notes below about [packing](#anc-textures-packing). |
| Texture Cube            | TC_       |            |                                  |
| Media Texture           | MT_       |            |                                  |
| Render Target           | RT_       |            |                                  |
| Cube Render Target      | RTC_     |            |                                  |
| Texture Light Profile   | TLP_     |            |                                  |


<a name="anc-textures-packing"></a>

#### 1.2.6.1 Texture Packing
It is common practice to pack multiple layers of texture data into one texture. An example of this is packing Emissive, Roughness, Ambient Occlusion together as the Red, Green, and Blue channels of a texture respectively. To determine the suffix, simply stack the given suffix letters from above together, e.g. `_ERO`.

> It is generally acceptable to include an Alpha/Opacity layer in your Diffuse/Albedo's alpha channel and as this is common practice, adding `A` to the `_D` suffix is optional.

Packing 4 channels of data into a texture (RGBA) is not recommended except for an Alpha/Opacity mask in the Diffuse/Albedo's alpha channel as a texture with an alpha channel incurs more overhead than one without.


<a name="anc-sdf"></a>

#### 1.2.7 Signed Distance Fields

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Signed Distance Field   | SDF_       |            |                                  |
| SDF (Elements)          | SDF_       | _E         |                                  |
| SDF (Icons)             | SDF_       | _I         |                                  |
| SDF (Alphabet)          | SDF_       | _A         |                                  |
| SDF (Furniture)         | SDF_       | _F         |                                  |


<a name="anc-shaders"></a>

#### 1.2.8 Shaders

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Shader (VFX)            | SH_        | _VFX       |                                  |
| Shader (Elements)       | SH_        | _E         |                                  |
| Shader (Icons)          | SH_        | _I         |                                  |
| Shader (Alphabet)       | SH_        | _A         |                                  |
| Shader (Furniture)      | SH_        | _F         |                                  |
| Shader (Level)          | SH_        | _L         |                                  |


<a name="anc-shader-graphs"></a>

#### 1.2.9 Shader Graphs

| Asset Type              | Prefix     | Suffix     | Notes                            |
| ----------------------- | ---------- | ---------- | -------------------------------- |
| Shader Graphs (VFX)       | SHG_       | _VFX       |                                  |
| Shader Graphs (Elements)  | SHG_       | _E         |                                  |
| Shader Graphs (Icons)     | SHG_       | _I         |                                  |
| Shader Graphs (Alphabet)  | SHG_       | _A         |                                  |
| Shader Graphs (Furniture) | SHG_       | _F         |                                  |
| Shader Graphs (Level)     | SHG_       | _L         |                                  |


<a name="anc-misc"></a>

#### 1.2.10 Miscellaneous

| Asset Type                      | Prefix | Suffix | Notes |
| ------------------------------- | ------ | ------ | ----- |
| Universal Render Pipeline Asset | URP_   |        |       |
| Post Process Volume Profile     | PP_    |        |       |
| User Interface                  | UI_    |        |       |
| Lighting Setings Asset          | LI_    |        |       |


<a name="anc-physics"></a>

#### 1.2.11 Physics

| Asset Type        | Prefix | Suffix | Notes |
| ----------------- | ------ | ------ | ----- |
| Physical Material | PM_    |        |       |


<a name="anc-audio"></a>

#### 1.2.12 Audio

| Asset Type     | Prefix | Suffix | Notes                                                        |
| -------------- | ------ | ------ | ------------------------------------------------------------ |
| Audio Clip     | A_     |        |                                                              |
| Audio Mixer    | MIX_   |        |                                                              |
| Dialogue Voice | DV_    |        |                                                              |
| Audio Class    |        |        | No prefix/suffix. Should be put in a folder called AudioClasses |


<a name="anc-ui"></a>

#### 1.2.13 User Interface
| Asset Type       | Prefix | Suffix | Notes |
| ---------------- | ------ | ------ | ----- |
| Font             | Font_  |        |       |
| Texture (Sprite) | T_     | _GUI   |       |


<a name="anc-effects"></a>

#### 4.2.14 Effects
| Asset Type              | Prefix | Suffix | Notes |
| ----------------------- | ------ | ------ | ----- |
| Particle System            | PS_     |        |       |
| Particle System (Elements) | PS_     | _E     |       |
| Particle System (Icons)    | PS_     | _I     |       |
| Particle System (Alphabet) | PS_     | _A     |       |
| Particle System (Furniture)| PS_     | _F     |       |
| Particle System (Levels)   | PS_     | _L     |       |
| Visual Effects             | VFX_    |        |       |
| Visual Effects (Elements)  | VFX_    | _E     |       |
| Visual Effects (Icons)     | VFX_    | _I     |       |
| Visual Effects (Alphabet)  | VFX_    | _A     |       |
| Visual Effects (Furniture) | VFX_    | _F     |       |
| Visual Effects (Levels)    | VFX_    | _L     |       |

# WHY?
These naming conventions can help with automation. The name can imply what import settings an asset gets, how it gets exported or who can add model behaviours. Remember to also change imported third party assets to these conventions.


**[⬆ Back to Top](#table-of-contents)**
