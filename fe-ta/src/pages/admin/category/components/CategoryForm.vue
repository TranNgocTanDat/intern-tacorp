<script lang="ts" setup>
import type { CategoryRequest, CategoryResponse } from "@/models/Category";
import type { PartnerResponse } from "@/models/Partner";
import { pa } from "element-plus/es/locales.mjs";
import { reactive, watch } from "vue";

// tạo props truyền dữ liệu từ component cha
const props = defineProps<{
  partners: PartnerResponse[];
  categories: CategoryResponse[];
  initialData?: CategoryResponse | null;
  mode?: "create" | "update";
  visible?: boolean;
}>();

// tạo emit để truyền sự kiện về component cha
const emit = defineEmits<{
  (e: "submit-form", request: CategoryRequest): void;
  (e: "update:visible", value: boolean): void;
}>();

// tạo đối tượng form với reactive để lắng nghe thay đổi dữ liệu
const form = reactive<CategoryRequest>({
  name: "",
  slug: "",
  parentId: 0,
  partnerId: undefined,
  description: "",
  orderIndex: 0,
  isActive: true,
  note: "",
});

// Theo dõi sự thay đổi của props.initialData để cập nhật lại form
watch(
  () => props.initialData,
  (val) => {
    if (val) {
      form.name = val.name ?? "";
      form.slug = val.slug ?? "";
      form.parentId = val.parentId ?? 0;
      form.partnerId = val.partnerId ?? undefined;
      form.description = val.description ?? "";
      form.orderIndex = val.orderIndex ?? 0;
      form.isActive = val.isActive ?? true;
      form.note = val.note ?? "";
    } else {
      // reset form when initialData is null
      form.name = "";
      form.slug = "";
      form.parentId = 0;
      form.partnerId = undefined;
      form.description = "";
      form.orderIndex = 0;
      form.isActive = true;
      form.note = "";
    }
  },
  { immediate: true }
);

// Xử lý sự kiện submit form
const onSubmit = () => {
  emit("submit-form", {
    name: form.name,
    slug: form.slug,
    parentId: form.parentId || 0,
    partnerId: form.partnerId,
    description: form.description,
    orderIndex: form.orderIndex,
    isActive: form.isActive,
    note: form.note,
  });
  if (props.mode === "create") {
    // reset form after submit if in create mode
    form.name = "";
    form.slug = "";
    form.parentId = 0;
    form.partnerId = undefined;
    form.description = "";
    form.orderIndex = 0;
    form.isActive = true;
    form.note = "";
  } else {
    // close form if in update mode
    emit("update:visible", false);
  }
};
</script>

<template>
  <el-dialog
    :title="props.mode === 'create' ? 'Thêm mới Category' : 'Cập nhật Category'"
    :model-value="props.visible"
    width="600px"
    @close="emit('update:visible', false)"
  >
    <el-form :model="form" label-width="120px">
      <el-form-item label="Name">
        <el-input v-model="form.name" placeholder="Nhập tên danh mục" />
      </el-form-item>
      <el-form-item label="Slug">
        <el-input v-model="form.slug" placeholder="Nhập slug" />
      </el-form-item>
      <el-form-item label="Danh mục cha">
        <el-select
          v-model="form.parentId"
          placeholder="Chọn danh mục cha"
          clearable
          :teleported="false"
        >
          <!-- Option mặc định nếu không chọn cha -->
          <el-option :value="0" label="Không có danh mục cha" />

          <!-- Các danh mục cha -->
          <el-option
            v-for="cat in categories"
            :key="cat.id"
            :label="cat.name"
            :value="cat.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Logo">
        <el-select
          v-model="form.partnerId"
          placeholder="Chọn logo"
          clearable
          :teleported="false"
        >
          <el-option
            v-for="p in partners"
            :key="p.id"
            :label="p.name"
            :value="p.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Description">
        <el-input
          type="textarea"
          v-model="form.description"
          placeholder="Nhập mô tả"
        />
      </el-form-item>
      
      <el-form-item label="Order Index">
        <el-input
          v-model.number="form.orderIndex"
          type="number"
          placeholder="Nhập Order Index"
        />
      </el-form-item>
      <el-form-item label="Is Active">
        <el-switch v-model="form.isActive" />
      </el-form-item>
      <el-form-item label="Note">
        <el-input
          type="textarea"
          v-model="form.note"
          placeholder="Nhập ghi chú"
        />
      </el-form-item>
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="emit('update:visible', false)">Hủy</el-button>
        <el-button type="primary" @click="onSubmit">
          {{ props.mode === "create" ? "Tạo" : "Cập nhật" }}
        </el-button>
      </span>
    </template>
  </el-dialog>
</template>

<style lang="css" scoped></style>
