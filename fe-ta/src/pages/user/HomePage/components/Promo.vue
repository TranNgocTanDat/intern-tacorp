<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useProductStore } from "@/store/productStore";

const productStore = useProductStore();
const slugList = ["iphone-17-pro", "samsung-galaxy-s25-utltra"];

const loading = ref(true);
const error = ref<string | null>(null);

const product1 = ref<any>(null);
const product2 = ref<any>(null);

onMounted(async () => {
  try {
    loading.value = true;

    product1.value = await productStore.getProductBySlug(slugList[0]);
    product2.value = await productStore.getProductBySlug(slugList[1]);

    console.log("✅ Product 1:", product1.value);
    console.log("✅ Product 2:", product2.value);
  } catch (err: any) {
    error.value = err.message || "Không thể tải sản phẩm.";
    console.error("❌ Lỗi khi load sản phẩm:", err);
  } finally {
    loading.value = false;
  }
});
</script>

<template>
  <section v-if="!loading && product1 && product2" class="apple-promo">
    <!-- Left: Sản phẩm 1 -->
    <div class="promo-card">
      <img
        :src="product1.mediaList?.[0]?.mediaFileUrl"
        :alt="product1.productName"
      />
      <div class="content">
        <h2 class="title">{{ product1.productName }}</h2>
        <p class="description">{{ product1.shortDescription }}</p>
        <a href="#" class="cta"> BUY NOW</a>
      </div>
    </div>

    <!-- Right: Sản phẩm 2 -->
    <div class="promo-card">
      <img
        :src="product2.mediaList?.[0]?.mediaFileUrl"
        :alt="product2.productName"
      />
      <div class="content">
        <h2 class="title">{{ product2.productName }}</h2>
        <p class="description">{{ product2.shortDescription }}</p>
        <a href="#" class="cta">BUY NOW</a>
      </div>
    </div>
  </section>

  <div v-else-if="loading" class="loading">Đang tải sản phẩm...</div>
  <div v-else-if="error" class="error">{{ error }}</div>
</template>

<style scoped>
.apple-promo {
  display: flex;
  width: 100%;
}

.promo-card {
  flex: 1 1 50%;
  min-height: 500px;
  margin: 0 1rem;
  color: white;
  position: relative;
  display: flex;
  align-items: flex-end;
  padding: 20px;
  background-size: cover;
  background-position: center;
  transition: transform 0.3s ease;
}

.promo-card:hover {
  transform: scale(1.01);
}

.promo-card img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
  z-index: -1;
  filter: brightness(0.7);
}

.content {
  max-width: 80%;
}

.title {
  font-size: 1.8rem;
  font-weight: bold;
  margin-bottom: 12px;
  text-transform: uppercase;
}

.description {
  font-size: 1rem;
  margin-bottom: 16px;
  text-transform: uppercase;
}

.cta {
  display: inline-block;
  padding: 5px 40px;
  border: 1px solid white;
  color: white;
  text-decoration: none;
  transition: background 0.3s ease;
  text-transform: uppercase;
}

.cta:hover {
  background: white;
  color: black;
}
</style>
