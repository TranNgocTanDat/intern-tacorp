<script setup lang="ts">
import { ref, onMounted, computed } from "vue";
import { useCategoryStore } from "@/store/categoryStore";
import type { ProductResponse } from "@/models/Product";
import { Carousel, Slide } from "vue3-carousel";
import "vue3-carousel/dist/carousel.css";

type TabKey = "apple" | "samsung" | "oppo";

const activeTab = ref<TabKey>("apple");
const categoryStore = useCategoryStore();

onMounted(async () => {
  await categoryStore.getCategoryChildren();
});

const products = computed<Record<TabKey, ProductResponse[]>>(() => {
  const result: Record<TabKey, ProductResponse[]> = {
    apple: [],
    samsung: [],
    oppo: [],
  };

  for (const cat of categoryStore.categoriesChildren) {
    const key = cat.slug as TabKey;
    if (key in result) {
      result[key] = cat.products || [];
    }
  }

  return result;
});
</script>

<template>
  <el-tabs v-model="activeTab">
    <el-tab-pane label="APPLE" name="apple"></el-tab-pane>
    <el-tab-pane label="SAMSUNG" name="samsung"></el-tab-pane>
    <el-tab-pane label="OPPO" name="oppo"></el-tab-pane>
    <div class="product-carousel-wrapper">
      <Carousel
        :items-to-show="5"
        :items-to-scroll="1"
        :transition="500"
        v-if="products[activeTab].length"
      >
        <Slide v-for="p in products[activeTab].slice(0, 5)" :key="p.id">
          <div
            class="slide-item"
            @click="$router.push(`/product/${p.slug}`)"
          >
            <img
              :src="
                p.mediaList?.find((m) => m.isPrimary)?.mediaFileUrl ||
                '/assets/images/no-image.jpg'
              "
              :alt="p.productName"
            />
            <div class="item-name">{{ p.productName }}</div>
            <div class="item-price">
              ${{ p.discountPrice || p.originalPrice }}
            </div>
          </div>
        </Slide>
      </Carousel>

      <p v-else>Không có sản phẩm</p>
    </div>
  </el-tabs>
</template>

<style scoped>
.product-carousel-wrapper {
  margin-top: 1rem;
}

.slide-item {
  padding: 6px;
  cursor: pointer;
  transition: transform 0.3s;
}
.slide-item:hover {
  color: #888;
}

.slide-item img {
  width: 100%;
  height: 280px;
  object-fit: cover;
  border-radius: 8px;
  box-shadow: 0 3px 8px rgba(0, 0, 0, 0.2);
}

.item-name {
  margin-top: 6px;
  font-weight: 600;
  font-family: monospace;
  font-size: 14px;
}

.item-price {
  font-size: 14px;
  color: #888;
}

:deep(.el-tabs__item) {
  width: 120px;
  font-family: "Roboto Mono", monospace;
  color: #000 !important; /* Màu text bình thường */
  font-size: 18px;
  font-weight: 500;
}

/* Màu cho tab active */
:deep(.el-tabs__item.is-active) {
  color: #000 !important; /* Đổi từ xanh → đen */
}

/* Gạch chân bên dưới tab active */
:deep(.el-tabs__active-bar) {
  background-color: #000 !important; /* Gạch chân màu đen */
  height: 2px !important;
  border: none !important;
}

/* Bỏ border dưới của thanh tabs */
:deep(.el-tabs__nav-wrap::after) {
  background-color: transparent !important;
}
</style>
