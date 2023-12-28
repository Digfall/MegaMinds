package com.example.a3d;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.a3d.databinding.DefectItemBinding;

import java.util.ArrayList;

public class DefectAdapter extends RecyclerView.Adapter<DefectAdapter.DefectHolder> {

    private ArrayList<Defect> defectList = new ArrayList<>();
    public class DefectHolder extends RecyclerView.ViewHolder {
        private DefectItemBinding binding;
        public DefectHolder(View item) {
            super(item);
            binding = DefectItemBinding.bind(item);
        }

        public void bind(Defect defect) {
            binding.image.setImageResource(defect.getImage());
            binding.title.setText(defect.getTitle());
            binding.description.setText(defect.getDescription());
        }
    }
    @Override
    public DefectHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.defect_item, parent, false);
        return new DefectHolder(view);
    }
    @Override
    public void onBindViewHolder(DefectHolder holder, int position) {
        holder.bind(defectList.get(position));
    }
    @Override
    public int getItemCount() {
        return defectList.size();
    }

    public void addDefect(Defect defect) {
        defectList.add(defect);
        notifyDataSetChanged();
    }
}
