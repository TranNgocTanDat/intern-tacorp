<script setup lang="ts">
import { useProductSpecStore } from "@/store/productSpecStore";
import { onMounted, ref } from "vue";

const activeTab = ref("specs");
const productSpecStore = useProductSpecStore();

const props = defineProps<{ productId: number }>();

onMounted(() => {
  if (props.productId) {
    productSpecStore.getProductSpecsByProductId(props.productId);
  }
});
const reviews = ref([
  {
    name: "Nguyễn Văn A",
    comment: "Sản phẩm rất tốt, hiệu năng mạnh!",
    rating: 5,
  },
  { name: "Trần Thị B", comment: "Pin dùng ổn, sạc nhanh ok!", rating: 4 },
]);
</script>

<template>
  <div class="product-detail-container">
    <el-tabs v-model="activeTab" class="product-tabs">
      <el-tab-pane label="Thông số kỹ thuật" name="specs">
        <el-skeleton v-if="productSpecStore.loading" :rows="5" animated />
        <el-descriptions v-else border column="1" class="spec-table">
          <el-descriptions-item
            v-for="spec in productSpecStore.specs"
            :key="spec.id"
            :label="spec.specKey"
          >
            {{ spec.specValue }}
          </el-descriptions-item>
        </el-descriptions>
      </el-tab-pane>

      <el-tab-pane label="Mô tả sản phẩm" name="description">
        <div class="description-content">
          <p>
            iPhone 15 Pro Max mang đến hiệu năng mạnh mẽ nhờ chip Apple A17 Pro,
            màn hình Super Retina XDR siêu sắc nét, và hệ thống camera chuyên
            nghiệp.
          </p>
        </div>
      </el-tab-pane>

      <el-tab-pane :label="`Đánh giá (${reviews.length})`" name="reviews">
        <div v-if="reviews.length">
          <div
            v-for="(review, index) in reviews"
            :key="index"
            class="review-item"
          >
            <strong>{{ review.name }}</strong>
            <p>{{ review.comment }}</p>
            <el-rate v-model="review.rating" disabled />
            <el-divider />
          </div>
        </div>
        <div v-else class="no-reviews">Chưa có đánh giá nào.</div>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<style scoped>
.product-detail-container {
  max-width: 100%;
  margin: 30px auto;
  background: #fff;
  border-radius: 10px;
  padding: 20px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.product-tabs {
  --el-color-primary: #000; /* Màu đen như Apple */
}

.spec-table {
  margin-top: 15px;
}

.description-content {
  font-size: 15px;
  line-height: 1.6;
  padding: 10px;
}

.review-item {
  margin-bottom: 15px;
}

.no-reviews {
  text-align: center;
  color: #999;
  margin-top: 20px;
}
</style>
