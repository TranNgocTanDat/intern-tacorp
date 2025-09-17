<script lang="ts" setup>
import { ref } from "vue";

interface Product {
  id: number;
  name: string;
  imageUrl: string;
}

const activities = ref<Product[]>([
  {
    id: 1,
    name: "TRAINING",
    imageUrl:
      "https://images.unsplash.com/photo-1598970434795-0c54fe7c0642?auto=format&fit=crop&w=400&q=80",
  },
  {
    id: 2,
    name: "CYCLING",
    imageUrl:
      "https://images.unsplash.com/photo-1508606572321-901ea4437073?auto=format&fit=crop&w=400&q=80",
  },
  {
    id: 3,
    name: "RUNNING",
    imageUrl:
      "https://images.unsplash.com/photo-1517649763962-0c623066013b?auto=format&fit=crop&w=400&q=80",
  },
  {
    id: 4,
    name: "WELLNESS",
    imageUrl:
      "https://images.unsplash.com/photo-1508214751196-bcfd4ca60f91?auto=format&fit=crop&w=400&q=80",
  },
  {
    id: 5,
    name: "SNOW",
    imageUrl:
      "https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=400&q=80",
  },
  {
    id: 6,
    name: "YOGA",
    imageUrl:
      "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0?auto=format&fit=crop&w=400&q=80",
  },
  {
    id: 7,
    name: "SWIMMING",
    imageUrl:
      "https://images.unsplash.com/photo-1507537509458-b8312d0f9896?auto=format&fit=crop&w=400&q=80",
  },
  {
    id: 8,
    name: "HIKING",
    imageUrl:
      "https://images.unsplash.com/photo-1504384308090-c894fdcc538d?auto=format&fit=crop&w=400&q=80",
  },
]);

// Hàm chia array thành các nhóm nhỏ
function chunkArray<T>(arr: T[], chunkSize: number): T[][] {
  const chunks: T[][] = [];
  for (let i = 0; i < arr.length; i += chunkSize) {
    chunks.push(arr.slice(i, i + chunkSize));
  }
  return chunks;
}

const slides = ref(chunkArray(activities.value, 4)); // mỗi slide 4 item
</script>

<template>
  <div class="product-hot-wrapper">
    <h3>SHOP BY ACTIVITY</h3>
    <el-carousel indicator-position="none" arrow="always" height="50vh">
      <el-carousel-item v-for="(group, index) in slides" :key="index">
        <div class="slide-group">
          <div v-for="item in group" :key="item.id" class="slide-item">
            <img :src="item.imageUrl" :alt="item.name" />
            <div class="item-name">{{ item.name }}</div>
          </div>
        </div>
      </el-carousel-item>
    </el-carousel>
  </div>
</template>

<style scoped>
.product-hot-wrapper {
  width: 100%;
  height: 100%;
  margin: auto;
}
.slide-group {
  display: flex;
  justify-content: space-between;
  padding: 0 10px;
}
.slide-item {
  flex: 1 1 0;
  margin: 0 6px;
  text-align: center;
  cursor: pointer;
}
.slide-item img {
  width: 100%;
  height: 45vh;
  object-fit: cover;
  border-radius: 6px;
}
.item-name {
  margin-top: 8px;
  font-weight: 600;
  font-family: monospace;
}
</style>
