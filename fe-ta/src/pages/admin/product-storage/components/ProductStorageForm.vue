<script lang="ts" setup>
import type { ProductResponse } from "@/models/Product";
import type {
  ProductStorageRequest,
  ProductStorageResponse,
} from "@/models/ProductStorage";
import { reactive, ref, watch } from "vue";

const props = defineProps<{
  products: ProductResponse[];
  initialData?: ProductStorageResponse | null;
  mode?: "create" | "update";
  visible?: boolean;
}>();

const emit = defineEmits<{
  (e: "submit-form", request: ProductStorageRequest): void;
  (e: "update:visible", value: boolean): void;
}>();

// ===== FORM STATE =====
const form = reactive<ProductStorageRequest>({
  productId: 0,
  storageName: "",
  additionalPrice: 0,
  isAvailable: true,
  note: "",
});

const selectedProductId = ref<number | null>(null);

// ===== RESET FORM =====
function resetForm() {
  form.storageName = "";
  form.additionalPrice = 0;
  form.isAvailable = true;
  form.note = "";
  selectedProductId.value = null;
}

// ===== SET FORM FROM RESPONSE =====
function setFormFromResponse(data: ProductStorageResponse) {
  form.storageName = data.storageName ?? "";
  form.additionalPrice = data.additionalPrice ?? 0;
  form.isAvailable = data.isAvailable ?? true;
  form.note = data.note ?? "";
  selectedProductId.value = data.productId ?? null;
}

// ===== WATCH INITIAL DATA =====
watch(
  () => props.initialData,
  (val) => {
    if (val) {
      setFormFromResponse(val);
    } else {
      resetForm();
    }
  },
  { immediate: true }
);

// ===== SUBMIT =====
const onSubmit = () => {
  if (!selectedProductId.value) {
    console.warn("ProductId is required");
    return;
  }

  const payload: ProductStorageRequest = {
    productId: selectedProductId.value,
    storageName: form.storageName,
    additionalPrice: form.additionalPrice,
    isAvailable: form.isAvailable,
    note: form.note,
  };

  emit("submit-form", payload);

  if (props.mode === "create") resetForm();
};
</script>

<template>
  <el-dialog
    :title="props.mode === 'create' ? 'Thêm dung lượng sản phẩm' : 'Chỉnh sửa dung lượng sản phẩm'"
    :model-value="props.visible"
    width="700px"
    @close="emit('update:visible', false)"
  >
    <el-form :model="form" label-width="120px" label-position="top">
      <!-- Chọn sản phẩm -->
      <el-form-item label="Sản phẩm">
        <el-select
          v-model="selectedProductId"
          placeholder="Chọn sản phẩm"
          clearable
          filterable
          :teleported="false"
        >
          <el-option
            v-for="p in products"
            :key="p.id"
            :label="p.productName"
            :value="p.id"
          />
        </el-select>
      </el-form-item>

      <!-- Tên dung lượng -->
      <el-form-item label="Tên dung lượng">
        <el-input v-model="form.storageName" placeholder="Ví dụ: 128GB, 256GB..." />
      </el-form-item>

      <!-- Giá cộng thêm -->
      <el-form-item label="Giá cộng thêm (VNĐ)">
        <el-input-number
          v-model="form.additionalPrice"
          :min="0"
          :step="50000"
          placeholder="0"
          style="width: 100%"
        />
      </el-form-item>

      <!-- Trạng thái khả dụng -->
      <el-form-item label="Trạng thái khả dụng">
        <el-switch
          v-model="form.isAvailable"
          active-text="Còn hàng"
          inactive-text="Hết hàng"
        />
      </el-form-item>

      <!-- Ghi chú -->
      <el-form-item label="Ghi chú">
        <el-input v-model="form.note" type="textarea" rows="3" />
      </el-form-item>
    </el-form>

    <!-- Footer -->
    <template #footer>
      <el-button @click="emit('update:visible', false)">Hủy</el-button>
      <el-button type="primary" @click="onSubmit">
        {{ props.mode === "create" ? "Tạo" : "Cập nhật" }}
      </el-button>
    </template>
  </el-dialog>
</template>
