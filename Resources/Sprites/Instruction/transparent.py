#%%
from PIL import Image

def transPNG(srcImageName, dstImageName):
    img = Image.open(srcImageName)
    img = img.convert("RGBA")
    datas = img.getdata()
    newData = list()
    for item in datas:
        if item[0] > 225 and item[1] > 225 and item[2] > 225:
            newData.append((255, 255, 255, 0))
        else:
            newData.append(item)
    img.putdata(newData)
    img.save(dstImageName, "PNG")


# %%
transPNG('123.png', '123.png')
transPNG('AttackSystem.png', 'AttackSystem.png')
transPNG('Changemode.png', 'Changemode.png')
transPNG('ElementInteractive.png', 'ElementInteractive.png')
transPNG('HeartGet.png', 'HeartGet.png')
transPNG('HeartPoint.png', 'HeartPoint.png')
transPNG('Index.png', 'Index.png')
transPNG('Instruction.png', 'Instruction.png')
transPNG('InstructionMode.png', 'InstructionMode.png')
transPNG('MagicType.png', 'MagicType.png')
transPNG('Move.png', 'Move.png')
transPNG('NormalAttack.png', 'NormalAttack.png')

transPNG('Special.png', 'Special.png')
transPNG('SummonCard.png', 'SummonCard.png')
# %%
transPNG('Transport.png', 'Transport.png')


# %%
transPNG('TransportInstruction.png', 'TransportInstruction.png')
# %%
