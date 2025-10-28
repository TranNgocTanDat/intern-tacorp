<script lang="ts" setup>
import { ref, watch } from "vue";
import type { ProductResponse } from "@/models/Product";

// import carousel
import "vue3-carousel/dist/carousel.css";
import { Carousel, Slide, Navigation } from "vue3-carousel";
import type { CategoryResponse } from "@/models/Category";
import categoryApi from "@/services/categoryApi";

const props = defineProps<{ categoryId: number | null }>();

const category = ref<CategoryResponse | null>(null);
const products = ref<ProductResponse[]>([]);

const getCategoryById = async (id: number) => {
  try {
    const response = await categoryApi.getCategoryById(id);
    category.value = response;
    products.value = response.products || [];
  } catch (error) {
    console.error("Lỗi khi lấy category:", error);
    category.value = null;
    products.value = [];
  }
};

// Theo dõi categoryId để gọi API khi thay đổi
watch(
  () => props.categoryId,
  async (id) => {
    if (id !== null) {
      await getCategoryById(id);
    } else {
      category.value = null;
      products.value = [];
    }
  },
  { immediate: true }
);
</script>

<template>
  <div class="product-hot-wrapper">
    <div class="top-hot">
      <h3 class="title">{{ category?.name }}</h3>
      <div class="see-more" v-if="category?.slug">
        <router-link :to="`/category/${category.slug}`"> Xem thêm </router-link>
      </div>
    </div>

    <Carousel
      :items-to-show="4"
      :items-to-scroll="1"
      :transition="500"
      pause-autoplay-on-hover
    >
      <Slide v-for="p in products" :key="p.id">
        <div class="slide-item">
          <img
            :src="
              p.mediaList?.find((m) => m.isPrimary)?.mediaFileUrl ||
              '/assets/images/no-image.jpg'
            "
            :alt="p.productName || 'No Name'"
          />
          <div class="item-name">{{ p.productName || "No Name" }}</div>
        </div>
      </Slide>

      <template #addons>
        <Navigation />
      </template>
    </Carousel>
  </div>
</template>

<style scoped>
.product-hot-wrapper {
  width: 100%;
}
.top-hot {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
}

.title {
  font-size: 1.5rem;
  font-weight: 700;
  margin: 1rem;
}

.slide-item {
  /* display: flex; */
  /* flex-direction: column; */
  /* align-items: center; */
  padding: 6px;
  cursor: pointer;
  transition: transform 0.3s;
}
.slide-item:hover {
  transform: scale(1.05);
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
}
.see-more {
  border: 0.5px solid #000000;
  padding: 4px 8px;
  border-radius: 4px;
}

.see-more a {
  color: #000000;
  text-decoration: none;
  font-weight: 600;
}

.see-more a:hover {
  text-decoration: underline;
}
</style>
