<script setup lang="ts">
import { ref, onMounted, computed, watch } from "vue";
import { useRoute } from "vue-router";
import { useProductStore } from "@/store/productStore";
import { useProductColorStore } from "@/store/productColorStore";
import { useAppStore } from "@/store/appStore";
import ProductSpec from "./components/ProductSpec.vue";

const route = useRoute();
const productStore = useProductStore();
const productColorStore = useProductColorStore();
const appStore = useAppStore();

const loading = ref(false);
const selectedImage = ref(0);
const selectedColor = ref<string | null>(null);
const selectedStorage = ref<string | null>(null);
const isFavorite = ref(false);

const product = computed(() => productStore.selectedProduct);

const toggleFavorite = () => (isFavorite.value = !isFavorite.value);
const formatPrice = (price?: number) =>
  price ? new Intl.NumberFormat("vi-VN").format(price) + "đ" : "Liên hệ";

onMounted(async () => {
  const slug = route.params.slug as string;
  if (!slug) return;
  loading.value = true;
  await productStore.getProductBySlug(slug);
  loading.value = false;

  const p = product.value;
  if (p?.colors?.length) {
    selectedColor.value = p.colors[0].colorName;
    await loadColorImages();
  }
  if (p?.storages?.length) selectedStorage.value = p.storages[0].storageName;
});

const colorImages = ref<any[]>([]);
const colorCache = ref<Record<string, any[]>>({});

const loadColorImages = async () => {
  if (!product.value || !selectedColor.value) return;
  if (colorCache.value[selectedColor.value]) {
    colorImages.value = colorCache.value[selectedColor.value];
    return;
  }

  const color = product.value.colors?.find(
    (c) => c.colorName === selectedColor.value
  );
  if (!color) return;

  await productColorStore.getProductColorByProductIdAndColorId(
    product.value.id,
    color.id
  );

  const imgs = productColorStore.productColors?.[0]?.mediaList ?? [];
  colorImages.value = imgs;
  colorCache.value[selectedColor.value] = imgs;
};

watch(selectedColor, async () => {
  await loadColorImages();
  selectedImage.value = 0; // reset ảnh được chọn khi đổi màu
});

// lọc ảnh theo màu (nếu có colorId hoặc colorCode)
const filteredImages = computed(() => {
  if (colorImages.value.length) return colorImages.value;
  return product.value?.mediaList ?? [];
});

const productMediaByType = computed(() => {
  const allMedia = product.value?.mediaList ?? [];
  return {
    main: allMedia.filter((m) => m.mediaType === "main"),
    description: allMedia.filter((m) => m.mediaType === "description"),
    banner: allMedia.filter((m) => m.mediaType === "banner"),
  };
});
</script>

<template>
  <div v-if="loading" class="text-center py-10">
    <el-skeleton :rows="6" animated />
  </div>

  <div v-else-if="product" class="container">
    <!-- Breadcrumb -->
    <div class="breadcrumb">
      <span @click.prevent="appStore.goHome">Trang chủ</span> >
      <span>Điện thoại</span> >
      <strong>{{ product.productName }}</strong>
    </div>

    <el-row :gutter="30">
      <!-- Gallery -->
      <el-col :span="11">
        <div class="image-main">
          <el-badge
            v-if="product.discount"
            :value="`GIẢM ${product.discount}%`"
            class="discount-badge"
          />
          <el-button
            icon="el-icon-star-off"
            circle
            class="favorite-btn"
            @click="toggleFavorite"
            :type="isFavorite ? 'danger' : 'default'"
          />
          <img
            :src="
              filteredImages[selectedImage]?.mediaFileUrl || '/placeholder.jpg'
            "
            alt="Ảnh sản phẩm"
            class="main-img"
          />
        </div>

        <div class="thumbnail-list">
          <img
            v-for="(m, i) in filteredImages"
            :key="i"
            :src="m.mediaFileUrl"
            :class="{ selected: selectedImage === i }"
            class="thumb-img"
            @click="selectedImage = i"
          />
        </div>
      </el-col>

      <!-- Info -->
      <el-col :span="13">
        <h1 class="title">{{ product.productName }}</h1>

        <div class="price-section">
          <div class="price-box">
            <span class="price">{{ formatPrice(product.discountPrice) }}</span>
            <span class="price-old">{{
              formatPrice(product.originalPrice)
            }}</span>
          </div>
          <h4>(Giá đã bao gồm VAT)</h4>
        </div>

        <!-- Chọn màu -->
        <div class="section" v-if="product.colors?.length">
          <p class="section-title">Chọn màu sắc:</p>
          <el-button
            v-for="color in product.colors"
            :key="color.id"
            @click="selectedColor = color.colorName"
            :class="{ active: selectedColor === color.colorName }"
            class="storage-btn"
          >
            {{ color.colorName }}
          </el-button>
        </div>

        <!-- Chọn dung lượng -->
        <div class="section" v-if="product.storages?.length">
          <p class="section-title">Dung lượng:</p>
          <el-button
            v-for="storage in product.storages"
            :key="storage.id"
            @click="selectedStorage = storage.storageName"
            :class="{ active: selectedStorage === storage.storageName }"
            class="storage-btn"
          >
            {{ storage.storageName }}
          </el-button>
        </div>

        <!-- Khuyến mãi -->
        <el-card class="promotions" v-if="product.note">
          <h3 class="title-promotions">Ưu đãi đặc biệt</h3>
          <div class="promotions-list">
            <span>
              <el-icon><Check /></el-icon>
              Giảm thêm 500.000đ khi thanh toán qua VNPAY
            </span>
            <span>
              <el-icon><Check /></el-icon>
              Tặng gói bảo hành VIP 12 tháng
            </span>
            <span>
              <el-icon><Check /></el-icon>

              Giảm thêm 10% cho Loa, Tai nghe, Máy tính bàn, TV (từ 10 triệu)
              khi mua Điện thoại/Laptop
            </span>
            <span>
              <el-icon><Check /></el-icon>
              Giảm đến 40% khi mua các gói bảo hành
            </span>
          </div>
        </el-card>

        <!-- Nút hành động -->
        <div class="actions">
          <el-button
            type="primary"
            size="large"
            icon="el-icon-shopping-cart-full"
            >MUA NGAY</el-button
          >
          <el-button plain size="large">Trả góp 0%</el-button>
          <el-button plain size="large" icon="el-icon-phone-outline"
            >Gọi tư vấn</el-button
          >
        </div>
      </el-col>
    </el-row>
    <!-- Thông số kỹ thuật -->

    <div v-if="product.specs?.length" class="spec">
      <div class="spec-box">
        <h2 class="section-title">Thông số kỹ thuật nổi bật</h2>
        <ul class="spec-list">
          <li v-for="spec in product.specs.slice(0, 4)" :key="spec.id">
            <strong>{{ spec.specKey }}:</strong>
            <span>{{ spec.specValue }}</span>
          </li>
        </ul>
      </div>
    </div>

    <ProductSpec :product-id="product.id" />
    <div class="description-section">
      <h2 class="section-title">Mô tả sản phẩm</h2>
      <div class="description-gallery">
        <div
          v-for="(m, i) in productMediaByType.description"
          :key="m.id"
          class="description-item"
          :class="{
            'full-width': (i + 1) % 3 === 0,
            'half-width': (i + 1) % 3 !== 0,
            'id-1': m.id === 1,
            'id-2': m.id === 2,
            'id-3': m.id === 3,
          }"
        >
          <div class="description-box">
            <div class="box-img">
              <p>{{ m.descriptionMedia }}</p>

              <img :src="m.mediaFileUrl" :alt="m.descriptionMedia" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <div v-else class="text-center py-10 text-gray-500">
    Không tìm thấy sản phẩm
  </div>
</template>

<style scoped>
.container {
  max-width: 70vw;
  margin: auto;
  padding: 20px;
}

.breadcrumb {
  font-size: 14px;
  margin-bottom: 20px;
  color: #666;
}

.image-main {
  position: relative;
  margin-bottom: 20px;
}
.main-img {
  width: 100%;
  height: 450px;
  background-size: contain;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}
.discount-badge {
  position: absolute;
  top: 10px;
  left: 10px;
}
.favorite-btn {
  position: absolute;
  top: 10px;
  right: 10px;
}
.thumbnail-list {
  display: flex;
  gap: 10px;
}
.thumb-img {
  width: 60px;
  height: 60px;
  object-fit: cover;
  border-radius: 6px;
  border: 2px solid transparent;
  cursor: pointer;
}
.thumb-img.selected {
  border-color: #409eff;
}
.title {
  margin: 0;
  font-size: 26px;
  font-weight: 700;
}

.price-section {
  margin: 20px 0;
  width: 60%;
  padding: 5px 10px;
  border: 1px solid #eee;
  border-radius: 6px;
  box-sizing: border-box;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}
.price-box {
  display: flex;
  align-items: baseline;
  gap: 10px;
  margin: 15px 0;
}
.price {
  font-size: 26px;
  color: #f56c6c;
  font-weight: bold;
}
.price-old {
  text-decoration: line-through;
  color: #999;
}

.storage-btn {
  background-color: #fff;
  color: #8a8a8a;
  width: 100px;
  border: 1px solid #ccc;
  transition: 0.25s;
  border-radius: 6px;
}

.storage-btn:hover {
  color: #000000;
}

.storage-btn.active {
  color: #000000;
  border-color: #000000;
  box-shadow: 0 2px 6px rgba(64, 158, 255, 0.3);
}

.section {
  margin: 20px 0;
}
.section-title {
  font-weight: 600;
  margin-bottom: 10px;
}
.promotions {
  margin-top: 20px;
}

.title-promotions {
  margin: 0;
  font-size: 20px;
  font-weight: 700;
  color: #f56c6c;
}

.promotions-list {
  display: flex;
  flex-direction: column;
  gap: 8px;
  margin-top: 10px;
}

.actions {
  margin-top: 20px;
  display: flex;
  gap: 10px;
  flex-wrap: wrap;
}

.spec {
  margin-top: 60px;
}

.spec-box {
  border: 1px solid #eee;
  padding: 20px 40px;
  border-radius: 2px;
  box-shadow: 0 3px 10px rgba(48, 47, 47, 0.3);
}

.spec-box h2 {
  margin-top: 0;
  margin-bottom: 20px;
}
.spec-list {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 20px;
  list-style: none;
  padding: 0;
  margin: 0;
}
.spec-list li {
  display: flex;
  flex-direction: column;
  margin-bottom: 5px;
  color: #444;
}

/* Description */
.description-section {
  margin-top: 40px;
}

.description-gallery {
  background-color: #4a4a4a;
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 15px;
  height: 500px;
}

.description-item {
  height: 100%;
  box-sizing: border-box;
}

.description-box {
  height: 100%;
  position: relative;
  border-radius: 8px;
  padding: 2rem;
}

.box-img {
  height: 100%;
  position: relative;
  text-align: center;
  background-color: #000000;
}

/* Ảnh full chiều rộng */
.description-box img {
  position: absolute;
  bottom: 10px; /* nằm sát mép dưới */
  left: 0;
  right: 0;
  margin: auto;
  width: 90%;
  height: auto;
  display: block;
  border-radius: 8px;
  object-fit: cover;
}

/* Phần mô tả nằm trên ảnh */
.description-box p {
  padding-left: 74px;
  padding-right: 74px;
  padding-top: 34px;
}

/* Ảnh full-width chiếm cả 2 cột */
.description-item.full-width {
  grid-column: span 2;
}
</style>
