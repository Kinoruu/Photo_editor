import cv2
import tensorflow as tf
import numpy as np
from PIL import Image
import matplotlib as plt
import os
import sys
import msvcrt

os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'

try:
    os.mkdir('found')
except FileExistsError:
    for root, dirs, files in os.walk('found'):
        for f in files:
            os.unlink(os.path.join(root, f))
try:
    os.mkdir('found_pad')
except FileExistsError:
    for root, dirs, files in os.walk('found_pad'):
        for f in files:
            os.unlink(os.path.join(root, f))


height_pad = 5  # best possible choosed
width_pad = 2  # best possible choosed
threshold = 245  # best possible choosed

categories = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
              'v', 'w', 'x', 'y', 'z']
model = tf.keras.models.load_model('ANNv2.model')

msvcrt.setmode (sys.stdin.fileno(), os.O_BINARY)
bitmap = sys.stdin.buffer.read()

output = open("/tmp/testpython.jpg","wb")
no_gray = output
pic2 = output
gray = output
output.write(bitmap)
output.close()
'''
no_gray = cv2.imread(filename='test5.png')
pic2 = cv2.imread(filename='test5.png')
gray = cv2.imread(filename='test5.png', flags=cv2.IMREAD_GRAYSCALE)
'''
gray = np.array(gray)
height, width = gray.shape
white = 0
for h in range(height):
    for w in range(width):
        if gray[h, w] == [255]:
            white = white + 1
if white < (height * width) / 2:
    gray = cv2.cv2.bitwise_not(gray)
ret, binary = cv2.threshold(gray, threshold, 255, cv2.THRESH_OTSU)
inverted_binary = ~binary
contours, hierarchy = cv2.findContours(inverted_binary, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)

# deleting shapes included in bigger ones
k = 0
found_letter = []
found_best = []
found_best_r = []
for c in contours:
    found_letter.append(c)


def selection(contours_sel):
    iter1 = 0
    for cs in contours_sel:
        iter1 = iter1 + 1
        iter2 = 0
        xs, ys, ws, hs = cv2.boundingRect(cs)
        found_best.append(cs)
        # if w < 10 or h < 5:
            # found_letter[iter2].
        for ds in contours_sel:
            iter2 = iter2 + 1
            if iter1 == iter2:
                pass
            else:
                xs2, ys2, ws2, hs2 = cv2.boundingRect(ds)
                if (xs >= xs2) and ((xs + ws) <= (xs2 + ws2)) and (ys >= ys2) and ((ys + hs) <= (ys2 + hs2)):
                    found_best.pop()
                    pass


def rev_selection(r_contours_sel):
    iter1 = 0
    for cs in reversed(r_contours_sel):
        iter1 = iter1 + 1
        iter2 = 0
        xs, ys, ws, hs = cv2.boundingRect(cs)
        found_best_r.append(cs)
        #if w < 10 or h < 5:
            #found_letter.pop()
        for ds in r_contours_sel:
            iter2 = iter2 + 1
            if iter1 == iter2:
                pass
            else:
                xs2, ys2, ws2, hs2 = cv2.boundingRect(ds)
                if (xs >= xs2) and ((xs + ws) <= (xs2 + ws2)) and (ys >= ys2) and ((ys + hs) <= (ys2 + hs2)):
                    found_best_r.pop()
                    pass


pic1 = no_gray
selection(found_letter)
number_of_found_letters = len(found_best)  # number of found shapes
print("OCR have found ", number_of_found_letters, " letters")
found_better = []

for c in found_best:
    x, y, w, h = cv2.boundingRect(c)
    found_better.append(c)
    if (cv2.contourArea(c)) > 100:
        cv2.rectangle(pic1, (x, y), (x + w, y + h), (77, 22, 174), 2)
    else:
        found_better.pop()
cv2.imwrite('All contours with bounding box.png', pic1)
number_of_found_letters = len(found_better)  # number of found shapes
print("OCR have found ", number_of_found_letters, " letters")


mser = cv2.MSER_create()
gray_pic2 = cv2.cvtColor(pic2, cv2.COLOR_BGR2GRAY)
vis = pic2.copy()
regions, _ = mser.detectRegions(gray_pic2)
hulls = [cv2.convexHull(p.reshape(-1, 1, 2)) for p in regions]
cv2.polylines(vis, hulls, 1, (0, 255, 0))

mask = np.zeros((pic2.shape[0], pic2.shape[1], 1), dtype=np.uint8)
mask = cv2.dilate(mask, np.ones((150, 150), np.uint8))
for contour in hulls:
    cv2.drawContours(mask, [contour], -1, (25, 25, 55), -1)
    x, y, w, h = cv2.boundingRect(contour)
    new_img = Image.new('RGBA', (w, h), (255, 255, 255, 0))
    im = pic2[y - height_pad:y + h + height_pad, x - width_pad:x + w + width_pad]
    im = Image.fromarray(im)
    # im.paste(pic2, new_img)
    im.save('try2.png')
    im = cv2.imread('try2.png')
    # cv2.imshow('try2.png', im)
    text_only = cv2.bitwise_and(pic2, pic2, mask=mask)
cv2.imwrite('try1.png', text_only)
regions, _ = mser.detectRegions(gray_pic2)

bounding_boxes = [cv2.boundingRect(p.reshape(-1, 1, 2)) for p in regions]
found_letter_pad = []


def resize_with_pad(image, target_width, target_height, pr):  # function adding pads to found shapes
    background = Image.new('RGBA', (target_width, target_height), (255, 255, 255, 255))
    offset = (round((target_width - image.width) / 2), round((target_height - image.height) / 2))
    background.paste(image, offset)
    background.save('found_pad/found_letter_pad_' + str(pr) + '.png')
    imgs = cv2.imread('found_pad/found_letter_pad_' + str(pr) + '.png')
    found_letter_pad.append(imgs)


def prepare(filepath):
    pic_size = 48
    img_array = cv2.imread(filepath, cv2.IMREAD_GRAYSCALE)
    new_array = cv2.resize(img_array, (pic_size, pic_size))
    # plt.imshow(new_array, cmap='gray')
    # plt.show()
    return new_array.reshape(-1, pic_size, pic_size, 1)


predicted_letters = []
p = 0
for c in found_better:
    x, y, w, h = cv2.boundingRect(c)
    p = p + 1
    im = gray[y - height_pad:y + h + height_pad, x - width_pad:x + w + width_pad]  # cutting shapes from input image
    cv2.imwrite('found/found_letter_' + str(p) + '.png', im)
    im = Image.fromarray(im)
    # result = Image.fromarray(result)
    im2 = cv2.imread('found/found_letter_' + str(p) + '.png')

    resize_with_pad(im, w + 40, h + 40, p)

for iterator in range(len(found_letter_pad)):
    iterator += 1
    img = 'found_pad/found_letter_pad_' + str(iterator) + '.png'
    # image = cv2.imread(img, 0)
    # cv2.imshow("Obraz1", image)

    # cv2.waitKey(0)
    # cv2.destroyAllWindows()
    # train_results = model.evaluate(X_train, y_train, batch_size=batch_size)
    # print(train_results)
    # prediction = model.predict_classes([prepare(img)])  #  removed from tensorflow 2.6
    p_img = prepare(img)
    prediction = model.predict(p_img)
    classes = np.argmax(prediction, axis=1)

    for i in classes:
        classes = categories[i]

    print("predicted letter: ", classes)
    predicted_letters.append(classes)


try:
    text_file = open("ocered_text.txt", "w")
except FileExistsError:
    text_file = open("ocered_text.txt", "r+")
    text_file.truncate(0)
    text_file.close()
    text_file = open("ocered_text.txt", "a")
for letter_x in range(len(predicted_letters)):
    print(predicted_letters[letter_x], sep=' ', end='', flush=True)
    text_file.write(predicted_letters[letter_x])
