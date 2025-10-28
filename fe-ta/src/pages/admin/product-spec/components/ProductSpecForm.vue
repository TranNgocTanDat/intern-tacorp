<script lang="ts" setup>
import type { ProductResponse } from "@/models/Product";
import type {
  ProductSpecRequest,
  ProductSpecResponse,
} from "@/models/ProductSpec";
import { reactive, ref, watch } from "vue";

const props = defineProps<{
  products: ProductResponse[];
  initialData?: ProductSpecResponse | null;
  mode?: "create" | "update";
  visible?: boolean;
}>();

const emit = defineEmits<{
  (e: "submit-form", productId: number, request: ProductSpecRequest): void;
  (e: "update:visible", value: boolean): void;
}>();

// form state
const form = reactive<ProductSpecRequest>({
  specKey: "",
  specValue: "",
  orderIndex: 0,
  note: "",
});

// reset form
function resetForm() {
  form.specKey = "";
  form.specValue = "";
  form.orderIndex = 0;
  form.note = "";

}

// set form data when editing
function setFormFromResponse(data: ProductSpecResponse) {
  form.specKey = data.specKey ?? "";
  form.specValue = data.specValue ?? "";
  form.orderIndex = data.orderIndex ?? 0;
  form.note = data.note ?? "";
}

const selectedProductId = ref<number | null>(null);

// auto fill when initialData changes
watch(
  () => props.initialData,
  (val) => {
    if (val) {
      setFormFromResponse(val);
      selectedProductId.value = val.productId ?? null;
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

  const payload: ProductSpecRequest = {
    specKey: form.specKey,
    specValue: form.specValue,
    orderIndex: form.orderIndex,
    note: form.note,
  };

  emit("submit-form", selectedProductId.value, payload);

  if (props.mode === "create") resetForm();
};
</script>

<template>
  <el-dialog
    :title="props.mode === 'create' ? 'Thêm thông số' : 'Chỉnh sửa thông số'"
    :model-value="props.visible"
    width="700px"
    @close="emit('update:visible', false)"
  >
    <el-form :model="form" label-width="120px" label-position="top">
      <el-form-item label="Sản phẩm">
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

      <el-form-item label="Tên thông số">
        <el-input v-model="form.specKey" />
      </el-form-item>

      <el-form-item label="Giá trị thông số">
        <el-input v-model="form.specValue" />
      </el-form-item>

      <el-form-item label="Thứ tự hiển thị">
        <el-input-number v-model="form.orderIndex" :min="0" />
      </el-form-item>

      <el-form-item label="Ghi chú">
        <el-input v-model="form.note" type="textarea" />
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
