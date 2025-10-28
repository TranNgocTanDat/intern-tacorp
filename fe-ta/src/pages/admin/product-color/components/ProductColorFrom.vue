<script lang="ts" setup>
import type { ProductResponse } from "@/models/Product";
import type {
  ProductColorRequest,
  ProductColorResponse,
} from "@/models/ProductColor";
import { reactive, ref, watch } from "vue";

const props = defineProps<{
  products: ProductResponse[];
  initialData?: ProductColorResponse | null;
  mode?: "create" | "update";
  visible?: boolean;
}>();

const emit = defineEmits<{
  (e: "submit-form",request: ProductColorRequest): void;
  (e: "update:visible", value: boolean): void;
}>();

// form state
const form = reactive<ProductColorRequest>({
  productId: 0,
  colorName: "",
  colorCode: "",
  isAvailable: true,
  note: "",
});

const selectedProductId = ref<number | null>(null);

// reset form
function resetForm() {
  form.colorName = "";
  form.colorCode = "";
  form.isAvailable = true;
  form.note = "";
  selectedProductId.value = null;
}

// set form data when editing
function setFormFromResponse(data: ProductColorResponse) {
  form.colorName = data.colorName ?? "";
  form.colorCode = data.colorCode ?? "";
  form.isAvailable = data.isAvailable ?? true;
  form.note = data.note ?? "";
  selectedProductId.value = data.productId ?? null;
}

// watch initialData
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

// submit
const onSubmit = () => {
  if (!selectedProductId.value) {
    console.warn("ProductId is required");
    return;
  }

  const payload: ProductColorRequest = {
    productId: selectedProductId.value,
    colorName: form.colorName,
    colorCode: form.colorCode,
    isAvailable: form.isAvailable,
    note: form.note,
  };

  emit("submit-form", payload);

  if (props.mode === "create") resetForm();
};
</script>

<template>
  <el-dialog
    :title="props.mode === 'create' ? 'Thêm màu sản phẩm' : 'Chỉnh sửa màu sản phẩm'"
    :model-value="props.visible"
    width="700px"
    @close="emit('update:visible', false)"
  >
    <el-form :model="form" label-width="120px" label-position="top">
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

      <el-form-item label="Tên màu">
        <el-input v-model="form.colorName" placeholder="Ví dụ: Trắng Titan" />
      </el-form-item>

      <el-form-item label="Mã màu (Color Code)">
        <el-input v-model="form.colorCode" placeholder="#FFFFFF" />
      </el-form-item>

      <el-form-item label="Trạng thái khả dụng">
        <el-switch
          v-model="form.isAvailable"
          active-text="Còn hàng"
          inactive-text="Hết hàng"
        />
      </el-form-item>

      <el-form-item label="Ghi chú">
        <el-input v-model="form.note" type="textarea" rows="3" />
      </el-form-item>
    </el-form>

    <template #footer>
      <el-button @click="emit('update:visible', false)">Hủy</el-button>
      <el-button type="primary" @click="onSubmit">
        {{ props.mode === "create" ? "Tạo" : "Cập nhật" }}
      </el-button>
    </template>
  </el-dialog>
</template>
