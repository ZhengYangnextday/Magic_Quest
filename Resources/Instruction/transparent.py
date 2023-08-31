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
transPNG('('+ str(10)+').png', '('+ str(10)+').png')# %%
