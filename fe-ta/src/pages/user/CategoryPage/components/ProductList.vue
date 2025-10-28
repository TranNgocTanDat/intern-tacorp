<script lang="ts" setup>
import type { ProductResponse } from "@/models/Product";

defineProps<{
  products: ProductResponse[];
}>();
</script>

<template>
  <div class="product-list">
    <div v-for="product in products" :key="product.id" class="product-card">
      <!-- Badge góc -->
      <div class="badges">
        <span  class="badge discount">
          Giảm 50%
        </span>

        <span v-if="product.isActive" class="badge new">Mới</span>
        <span v-if="product.isActive" class="badge preorder">Nhận Đặt Cọc</span>
      </div>

      <!-- Ảnh sản phẩm -->
      <img
        :src="product.mediaList?.[0]?.mediaFileUrl || '/assets/images/no-image.jpg'"
        :alt="product.productName"
        class="product-image"
      />

      <!-- Tên sản phẩm -->
      <div class="product-name">{{ product.productName }}</div>

      <!-- Giá -->
      <div class="product-price">
        <span class="new-price">{{ formatCurrency(product.originalPrice) }}</span>
        <span v-if="product.originalPrice && product.discountPrice !== undefined && product.discountPrice < product.originalPrice" class="old-price">
          {{ formatCurrency(product.originalPrice) }}
        </span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
function formatCurrency(value?: number) {
  if (!value) return "0₫";
  return value.toLocaleString("vi-VN") + "₫";
}
</script>

<style scoped>
.product-list {
  display: grid;
  grid-template-columns: repeat(4, minmax(240px, 1fr));
  gap: 20px;
}

.product-card {
  position: relative;
  border-radius: 8px;
  overflow: hidden;
  background: #fff;
  box-shadow: 0 3px 10px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s;
  cursor: pointer;
}

.product-card:hover {
  transform: translateY(-5px);
}

/* Badge góc */
.badges {
  position: absolute;
  top: 8px;
  left: 8px;
  display: flex;
  flex-direction: column;
  gap: 4px;
  z-index: 2;
}

.badge {
  display: inline-block;
  padding: 2px 8px;
  border-radius: 8px;
  font-size: 12px;
  font-weight: bold;
  color: #fff;
}

.badge.discount {
  background: #d50000;
}

.badge.new {
  background: #1faa00;
}

.badge.preorder {
  background: #1976d2;
}

/* Ảnh */
.product-image {
  width: 100%;
  height: 240px;
  object-fit: cover;
}

/* Tên */
.product-name {
  font-weight: 600;
  text-align: center;
  margin: 8px;
  color: #000;
}

/* Giá */
.product-price {
  text-align: center;
  margin-bottom: 8px;
}

.new-price {
  color: #007bff;
  font-weight: bold;
  margin-right: 6px;
}

.old-price {
  text-decoration: line-through;
  color: #888;
  font-size: 0.9em;
}
</style>
